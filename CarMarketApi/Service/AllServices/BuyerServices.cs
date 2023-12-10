using CarMarketApi.CustomResponses;
using CarMarketApi.Data;
using CarMarketApi.Dtos;
using CarMarketApi.Dtos.BuyerDtos;
using CarMarketApi.Dtos.ItemDtos;
using CarMarketApi.Dtos.SellerDtos;
using CarMarketApi.Entities;
using CarMarketApi.Repository;
using CarMarketApi.Repository.RepositoryAbstracts;
using CarMarketApi.Service.ServiceAbstracts;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace CarMarketApi.Service.AllServices
{
    public class BuyerServices : IBuyerService
    {
        private readonly Context _context;
        private readonly IRepositories _repository;

        public BuyerServices(IRepositories repositories, Context context)
        {
            _context = context;
            _repository = repositories;
        }

        public async Task<CustomApiResponses<GetDtosWithCount<List<BuyerGetDto>>>> GetBuyersAsync(BuyerFilterDto filter, CancellationToken cancellationToken)
        {
            var buyers = await _repository.BuyerRepository.GetBuyersWithRelatedDataAsync(cancellationToken);

            if (buyers == null)
            {
                throw new CustomExceptions.NoContentException("No content");
            }

            var filteredBuyers = FilterData(filter, buyers);

            if (filteredBuyers.Count == 0)
            {
                throw new CustomExceptions.NotFoundException("Buyers not found");
            }

            var buyerDtos = filteredBuyers.Select(buyer => new BuyerGetDto
            {
                Id = buyer.Id,
                Name = buyer.Name,
                Surname = buyer.Surname,
                Age = buyer.Age,
                PersonalInformation = buyer.PersonalInformation != null
                                      ? new Dtos.PersonalInformationDtos.PersonalInformationOnlyDto
                                      {
                                          Id = buyer.PersonalInformation.Id,
                                          PhoneNumber = buyer.PersonalInformation.PhoneNumber,
                                          EmailAddress = buyer.PersonalInformation.EmailAddress
                                      }
                                      : null,
                Items = buyer.Items != null
                        ? buyer.Items.Select(i => new ItemOnlyDto
                        {
                            Type = i.Type,
                            Cost = i.Cost
                        }).ToList()
                        : new List<ItemOnlyDto>(),
                Sellers = buyer.SellersBuyers != null
                          ? buyer.SellersBuyers.Where(sb => sb.Seller != null).Select(s => new Dtos.SellerDtos.SellerOnlyDto
                          {
                              Id = s.Seller.Id,
                              Name = s.Seller.Name,
                              Surname = s.Seller.Surname,
                              Age = s.Seller.Age
                          }).ToList()
                          : new List<SellerOnlyDto>()

            })
                .OrderByDescending(b => b.Id)
                .Skip(filter.Offset ?? 0)
                .Take(filter.Limit ?? 10)
                .ToList();

            return CustomApiResponses<GetDtosWithCount<List<BuyerGetDto>>>.SuccesResult(new GetDtosWithCount<List<BuyerGetDto>>
            {
                Data = buyerDtos,
                Count = filteredBuyers.Count()
            });

        }

        public List<Buyer> FilterData(BuyerFilterDto filter, IQueryable<Buyer> buyers)
        {
            if (filter.Id != null)
            {
                buyers = buyers.Where(b => b.Id == filter.Id);
            }
            if (!filter.Name.IsNullOrEmpty())
            {
                buyers = buyers.Where(b => b.Name.Contains(filter.Name));
            }
            if (!filter.Surname.IsNullOrEmpty())
            {
                buyers = buyers.Where(b => b.Surname.Contains(filter.Surname));
            }
            if (filter.Age != null)
            {
                buyers = buyers.Where(b => b.Age == filter.Age);
            }
            if (filter.PersonalInformationId != null)
            {
                buyers = buyers.Where(b => b.PersonalInformation.Id == filter.PersonalInformationId);
            }
            if (filter.SellerIds != null && filter.SellerIds.Any())
            {
                buyers = buyers.Where(b => b.SellersBuyers
                               .Select(sb => sb.SellerId)
                               .Any(sellerId => filter.SellerIds.Contains((int)sellerId)));

            }
            if (filter.ItemIds != null && filter.ItemIds.Any())
            {
                buyers = buyers.Where(b => b.Items
                               .Select(i => i.Id)
                               .Any(itemId => filter.ItemIds.Contains(itemId)));
            }
            return buyers.ToList();
        }

        public async Task<CustomApiResponses<BuyerGetDto>> CrreateBuyerAsync(BuyerPostDto input, CancellationToken cancellationToken)
        {
            var buyer = new Buyer
            {
                Name = input.Name,
                Surname = input.Surname,
                Age = (int)input.Age
            };

            if (!input.SellerIds.IsNullOrEmpty())
            {
                buyer.SellersBuyers = new List<SellersBuyersJoin>();

                foreach (var sellerId in input.SellerIds)
                {
                    buyer.SellersBuyers.Add(new SellersBuyersJoin
                    {
                        SellerId = sellerId,
                        BuyerId = buyer.Id
                    });
                }
            }
            if (!input.ItemIds.IsNullOrEmpty())
            {
                buyer.Items = new List<Item>();

                foreach(var itemId in input.ItemIds)
                {
                    buyer.Items.Add(new Item
                    {
                        Id = itemId
                    });
                }
            }

            if (input.PhoneNumber != null || input.EmailAddress != null)
            {
                buyer.PersonalInformation = new BuyerPersonalInformation
                {
                    PhoneNumber = (int)input.PhoneNumber,
                    EmailAddress = input.EmailAddress
                };
            }

            var personalIformation = await _context.BuyersPersonalInformations
                                                   .Where(pi => pi.Buyer.Id == buyer.Id)
                                                   .FirstOrDefaultAsync(cancellationToken);
            if (personalIformation != null)
            {
                buyer.PersonalInformation.Id = personalIformation.Id;
            }


            await _repository.BuyerRepository.CreateBuyerAsync(buyer, cancellationToken);
            await _repository.BuyerRepository.SaveChangesAsync(cancellationToken);

            var buyerQueryable = await _repository.BuyerRepository.GetBuyersAsync(cancellationToken);
            var fetchedBuyer = await buyerQueryable
                                     .Include(b => b.PersonalInformation)
                                     .Include(b => b.Items)
                                     .Include(b => b.SellersBuyers)
                                            .ThenInclude(sb => sb.Seller)
                                    .FirstOrDefaultAsync(b => b.Id == buyer.Id, cancellationToken);

            if (fetchedBuyer == null)
            {
                throw new CustomExceptions.NotFoundException("Buyer not found");

            }

            var buyerDto = new BuyerGetDto
            {
                Id = fetchedBuyer.Id,
                Name = fetchedBuyer.Name,
                Surname = fetchedBuyer.Surname,
                Age = fetchedBuyer.Age,
                PersonalInformation = fetchedBuyer.PersonalInformation != null
                                      ? new Dtos.PersonalInformationDtos.PersonalInformationOnlyDto
                                      {
                                          Id = fetchedBuyer.PersonalInformation.Id,
                                          PhoneNumber = fetchedBuyer.PersonalInformation.PhoneNumber,
                                          EmailAddress = fetchedBuyer.PersonalInformation.EmailAddress
                                      }
                                      : null,
                Items = fetchedBuyer.Items != null
                        ? fetchedBuyer.Items.Select(i => new ItemOnlyDto
                        {
                            Type = i.Type,
                            Cost = i.Cost
                        }).ToList()
                        : new List<ItemOnlyDto>(),
                Sellers = fetchedBuyer.SellersBuyers != null
                          ? fetchedBuyer.SellersBuyers.Where(sb => sb.Seller != null).Select(s => new Dtos.SellerDtos.SellerOnlyDto
                          {
                              Id = s.Seller.Id,
                              Name = s.Seller.Name,
                              Surname = s.Seller.Surname,
                              Age = s.Seller.Age
                          }).ToList()
                          : new List<SellerOnlyDto>()

            };

            return CustomApiResponses<BuyerGetDto>.SuccesResult(buyerDto);
        }

        public async Task<CustomApiResponses<string>> UpdateBuyerAsync(BuyerPutDto input, CancellationToken cancellationToken)
        {
            var buyerQueryable = await _repository.BuyerRepository.GetBuyersAsync(cancellationToken);
            var buyer = await buyerQueryable.AsQueryable()
                                        .Include(b => b.Items)
                                        .Include(b => b.PersonalInformation)
                                        .Include(b => b.SellersBuyers)
                                        .Where(b => b.Id == input.Id)
                                        .FirstOrDefaultAsync(cancellationToken);
            buyer.Id = input.Id.HasValue ? (int)input.Id.Value : buyer.Id;
            if(input.Name != null)
            {
                buyer.Name = input.Name;
            }
            else
            {
                buyer.Name = buyer.Name;
            }
            if(input.Surname != null)
            {
                buyer.Surname = input.Surname;
            }
            else
            {
                buyer.Surname = buyer.Surname;
            }
            buyer.Age = input.Age.HasValue ? (int)input.Age.Value : buyer.Age;
            if(buyer.PersonalIformationId == null)
            {
                buyer.PersonalInformation = new BuyerPersonalInformation
                {
                    PhoneNumber = (int)input.PhoneNumber,
                    EmailAddress = input.EmailAddress
                };
            }
            else
            {
                buyer.PersonalInformation.PhoneNumber = input.PhoneNumber.HasValue ? (int)input.PhoneNumber.Value : buyer.PersonalInformation.PhoneNumber;
                if (input.EmailAddress != null)
                {
                    buyer.PersonalInformation.EmailAddress = input.EmailAddress;
                }
                else
                {
                    buyer.PersonalInformation.EmailAddress = buyer.PersonalInformation.EmailAddress;
                }
            }
            


            await _repository.BuyerRepository.UpdateBuyerAsync(buyer, cancellationToken);

            if (!input.SellerIds.IsNullOrEmpty())
            {
                foreach (var sellerId in input.SellerIds)
                {
                    buyer.SellersBuyers.Clear();
                    buyer.SellersBuyers.Add(new SellersBuyersJoin
                    {
                        SellerId = sellerId,
                        BuyerId = buyer.Id
                    });
                }
            }
            if(!input.ItemIds.IsNullOrEmpty())
            {
                foreach(var itemId in input.ItemIds)
                {
                    buyer.Items.Clear();
                    buyer.Items.Add(new Item
                    {
                        Id = itemId
                    });
                }
            }

            if (buyer == null)
            {
                throw new CustomExceptions.NotFoundException("Buyer not found");
            }

            await _repository.BuyerRepository.SaveChangesAsync(cancellationToken);

            return CustomApiResponses<string>.SuccesResult("Buyer changed successfully");

        }

        public async Task<CustomApiResponses<string>> DeleteBuyerAsync(int buyerId, CancellationToken cancellationToken)
        {
            var buyer = await _repository.BuyerRepository.GetBuyerByIdAsync(buyerId, cancellationToken);

            if(buyer == null)
            {
                throw new CustomExceptions.NotFoundException("Buyer not found on that Id");
            }

            if(await _repository.BuyerRepository.DeleteBuyerAsync(buyerId, cancellationToken) &&
               await _repository.BuyerRepository.DeleteSellersBuyersAsync(buyerId, cancellationToken))
            {
                await _repository.BuyerRepository.SaveChangesAsync(cancellationToken);
                return CustomApiResponses<string>.SuccesResult("Buyer deleted successfully");
            }
            throw new Exception();

        }
    }
}
