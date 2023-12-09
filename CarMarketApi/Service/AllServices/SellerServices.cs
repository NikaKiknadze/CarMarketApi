using CarMarketApi.CustomResponses;
using CarMarketApi.Data;
using CarMarketApi.Dtos;
using CarMarketApi.Dtos.BuyerDtos;
using CarMarketApi.Dtos.ItemDtos;
using CarMarketApi.Dtos.SellerDtos;
using CarMarketApi.Entities;
using CarMarketApi.Repository;
using CarMarketApi.Service.ServiceAbstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarMarketApi.Service.AllServices
{
    public class SellerServices : ISellerService
    {
        private readonly Context _context;
        private readonly IRepositories _repository;

        public SellerServices(IRepositories repositories, Context context)
        {
            _context = context;
            _repository = repositories;
        }

        public async Task<CustomApiResponses<GetDtosWithCount<List<SellerGetDto>>>> GetSellersAsync(SellerFilterDto filter, CancellationToken cancellationToken)
        {
            var sellers = await _repository.SellerRepository.GetSellersRelatedDataAsync(cancellationToken);

            if (sellers == null)
            {
                throw new CustomExceptions.NoContentException("No content");
            }

            var filteredSellers = FilterData(filter, sellers);

            if (filteredSellers.Count == 0)
            {
                throw new CustomExceptions.NotFoundException("Buyers not found");
            }

            var sellerDtos = filteredSellers.Select(seller => new SellerGetDto
            {
                Id = seller.Id,
                Name = seller.Name,
                Surname = seller.Surname,
                Age = seller.Age,
                PersonalInformation = seller.SellerPersonalInformation != null
                                      ? new Dtos.PersonalInformationDtos.PersonalInformationOnlyDto
                                      {
                                          Id = seller.SellerPersonalInformation.Id,
                                          PhoneNumber = seller.SellerPersonalInformation.PhoneNumber,
                                          EmailAddress = seller.SellerPersonalInformation.EmailAddress
                                      }
                                      : null,
                Items = seller.Items != null
                        ? seller.Items.Select(i => new ItemOnlyDto
                        {
                            Type = i.Type,
                            Cost = i.Cost
                        }).ToList()
                        : new List<ItemOnlyDto>(),
                Buyers = seller.SellersBuyers != null
                          ? seller.SellersBuyers.Where(sb => sb.Buyer != null).Select(b => new BuyerOnlyDto
                          {
                              Id = b.Buyer.Id,
                              Name = b.Buyer.Name,
                              Surname = b.Buyer.Surname,
                              Age = b.Buyer.Age
                          }).ToList()
                          : new List<BuyerOnlyDto>()

            })
                .OrderByDescending(s => s.Id)
                .Skip(filter.Offset ?? 0)
                .Take(filter.Limit ?? 10)
                .ToList();

            return CustomApiResponses<GetDtosWithCount<List<SellerGetDto>>>.SuccesResult(new GetDtosWithCount<List<SellerGetDto>>
            {
                Data = sellerDtos,
                Count = filteredSellers.Count()
            });
        }

        public List<Seller> FilterData(SellerFilterDto filter, IQueryable<Seller> sellers)
        {
            if (filter.Id != null)
            {
                sellers = sellers.Where(b => b.Id == filter.Id);
            }
            if (filter.Name.IsNullOrEmpty())
            {
                sellers = sellers.Where(b => b.Name.Contains(filter.Name));
            }
            if (filter.Surname.IsNullOrEmpty())
            {
                sellers = sellers.Where(b => b.Surname.Contains(filter.Surname));
            }
            if (filter.Age != null)
            {
                sellers = sellers.Where(b => b.Age == filter.Age);
            }
            if (filter.PersonalInformationId != null)
            {
                sellers = sellers.Where(s => s.PersonalInformationId == filter.PersonalInformationId);
            }
            if (filter.BuyerIds != null && filter.BuyerIds.Any())
            {
                sellers = sellers.Where(b => b.SellersBuyers
                               .Select(sb => sb.BuyerId)
                               .Any(buyerId => filter.BuyerIds.Contains((int)buyerId)));

            }
            if (filter.ItemIds != null && filter.ItemIds.Any())
            {
                sellers = sellers.Where(s => s.Items
                               .Select(i => i.Id)
                               .Any(itemId => filter.ItemIds.Contains(itemId)));
            }
            return sellers.ToList();
        }

        public async Task<CustomApiResponses<SellerGetDto>> CreateSellerAsync(SellerPostDto input, CancellationToken cancellationToken)
        {
            var seller = new Seller
            {
                Name = input.Name,
                Surname = input.Surname,
                Age = (int)input.Age
            };

            if (!input.BuyerIds.IsNullOrEmpty())
            {
                seller.SellersBuyers = new List<SellersBuyersJoin>();

                foreach (var buyerId in input.BuyerIds)
                {
                    seller.SellersBuyers.Add(new SellersBuyersJoin
                    {
                        BuyerId = buyerId,
                        SellerId = seller.Id
                    });
                }
            }
            if (!input.ItemIds.IsNullOrEmpty())
            {
                seller.Items = new List<Item>();

                foreach (var itemId in input.ItemIds)
                {
                    seller.Items.Add(new Item
                    {
                        Id = itemId
                    });
                }
            }

            if (input.PhoneNumber != null || input.EmailAddress != null)
            {
                seller.SellerPersonalInformation = new SellerPersonalInformation
                {
                    PhoneNumber = (int)input.PhoneNumber,
                    EmailAddress = input.EmailAddress
                };
            }

            var personalIformation = await _context.SellersPersonalInformations
                                                   .Where(pi => pi.SellerId == seller.Id)
                                                   .FirstOrDefaultAsync(cancellationToken);
            if (personalIformation != null)
            {
                seller.PersonalInformationId = personalIformation.Id;
            }


            await _repository.SellerRepository.CreateSellerAsync(seller, cancellationToken);
            await _repository.SellerRepository.SaveChangesAsync(cancellationToken);

            var sellerQueryable = await _repository.SellerRepository.GetSellersAsync(cancellationToken);
            var fetchedSeller = await sellerQueryable
                                     .Include(s => s.SellerPersonalInformation)
                                     .Include(s => s.Items)
                                     .Include(s => s.SellersBuyers)
                                            .ThenInclude(sb => sb.Buyer)
                                    .FirstOrDefaultAsync(s => s.Id == seller.Id, cancellationToken);

            if (fetchedSeller == null)
            {
                throw new CustomExceptions.NotFoundException("Seller not found");

            }

            var sellerDto = new SellerGetDto
            {
                Id = fetchedSeller.Id,
                Name = fetchedSeller.Name,
                Surname = fetchedSeller.Surname,
                Age = fetchedSeller.Age,
                PersonalInformation = fetchedSeller.SellerPersonalInformation != null
                                      ? new Dtos.PersonalInformationDtos.PersonalInformationOnlyDto
                                      {
                                          Id = fetchedSeller.SellerPersonalInformation.Id,
                                          PhoneNumber = fetchedSeller.SellerPersonalInformation.PhoneNumber,
                                          EmailAddress = fetchedSeller.SellerPersonalInformation.EmailAddress
                                      }
                                      : null,
                Items = fetchedSeller.Items != null
                        ? fetchedSeller.Items.Select(i => new ItemOnlyDto
                        {
                            Type = i.Type,
                            Cost = i.Cost
                        }).ToList()
                        : new List<ItemOnlyDto>(),
                Buyers = fetchedSeller.SellersBuyers != null
                          ? fetchedSeller.SellersBuyers.Where(sb => sb.Buyer != null).Select(s => new BuyerOnlyDto
                          {
                              Id = s.Buyer.Id,
                              Name = s.Buyer.Name,
                              Surname = s.Buyer.Surname,
                              Age = s.Buyer.Age
                          }).ToList()
                          : new List<BuyerOnlyDto>()

            };

            return CustomApiResponses<SellerGetDto>.SuccesResult(sellerDto);
        }

        public async Task<CustomApiResponses<string>> UpdateSellerAsync(SellerPutDto input, CancellationToken cancellationToken)
        {
            var sellerQueryable = await _repository.SellerRepository.GetSellersAsync(cancellationToken);
            var seller = await sellerQueryable.AsQueryable()
                                        .Include(s => s.Items)
                                        .Include(s => s.SellerPersonalInformation)
                                        .Include(s => s.SellersBuyers)
                                        .Where(s => s.Id == input.Id)
                                        .FirstOrDefaultAsync(cancellationToken);
            seller.Id = input.Id.HasValue ? (int)input.Id.Value : seller.Id;
            seller.Name = input.Name.IsNullOrEmpty() ? input.Name : seller.Name;
            seller.Surname = input.Surname.IsNullOrEmpty() ? input.Surname : seller.Surname;
            seller.Age = input.Age.HasValue ? (int)input.Age.Value : seller.Age;
            seller.SellerPersonalInformation.PhoneNumber = input.PhoneNumber.HasValue ? (int)input.PhoneNumber.Value : seller.SellerPersonalInformation.PhoneNumber;
            seller.SellerPersonalInformation.EmailAddress = input.EmailAddress.IsNullOrEmpty() ? input.EmailAddress : seller.SellerPersonalInformation.EmailAddress;


            await _repository.SellerRepository.UpdateSellerAsync(seller, cancellationToken);

            if (!input.BuyerIds.IsNullOrEmpty())
            {
                foreach (var buyerId in input.BuyerIds)
                {
                    seller.SellersBuyers.Clear();
                    seller.SellersBuyers.Add(new SellersBuyersJoin
                    {
                        BuyerId = buyerId,
                        SellerId = seller.Id
                    });
                }
            }
            if (!input.ItemIds.IsNullOrEmpty())
            {
                foreach (var itemId in input.ItemIds)
                {
                    seller.Items.Clear();
                    seller.Items.Add(new Item
                    {
                        Id = itemId
                    });
                }
            }

            if (seller == null)
            {
                throw new CustomExceptions.NotFoundException("Seller not found");
            }

            await _repository.SellerRepository.SaveChangesAsync(cancellationToken);

            return CustomApiResponses<string>.SuccesResult("Seller changed successfully");
        }

        public async Task<CustomApiResponses<string>> DeleteSellerAsync(int sellerId, CancellationToken cancellationToken)
        {
            var seller = await _repository.SellerRepository.GetSellerByIdAsync(sellerId, cancellationToken);

            if (seller == null)
            {
                throw new CustomExceptions.NotFoundException("Seller not found on that Id");
            }

            if (await _repository.SellerRepository.DeleteSellerAsync(sellerId, cancellationToken) &&
               await _repository.SellerRepository.DeleteSellersBuyersAsync(sellerId, cancellationToken))
            {
                await _repository.SellerRepository.SaveChangesAsync(cancellationToken);
                return CustomApiResponses<string>.SuccesResult("Seller deleted successfully");
            }
            throw new Exception();
        }
    }
}
