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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace CarMarketApi.Service.AllServices
{
    public class ItemService : IItemService
    {
        private readonly Context _context;
        private readonly IRepositories _repository;

        public ItemService(IRepositories repository, Context context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<CustomApiResponses<GetDtosWithCount<List<ItemGetDto>>>> GetItemsAsync(ItemFilterDto filter, CancellationToken cancellationToken)
        {
            var items = await _repository.ItemRepository.GetItemsRelatedDataAsync(cancellationToken);

            if (items == null)
            {
                throw new CustomExceptions.NoContentException("No content");
            }

            var filteredItems = FilterData(filter, items);

            if (filteredItems.Count == 0)
            {
                throw new CustomExceptions.NotFoundException("Items not found");
            }

            var itemDtos = filteredItems.Select(item => new ItemGetDto
            {
                Id = item.Id,
                Type = item.Type,
                Cost = item.Cost,
                Buyer = item.Buyer != null
                                      ? new BuyerOnlyDto
                                      {
                                          Id = item.Buyer.Id,
                                          Name = item.Buyer.Name,
                                          Surname = item.Buyer.Surname,
                                          Age = item.Buyer.Age
                                      }
                                      : null,
                Seller = item.Seller != null
                                      ? new SellerOnlyDto
                                      {
                                          Id = item.Seller.Id,
                                          Name = item.Seller.Name,
                                          Surname = item.Seller.Surname,
                                          Age = item.Seller.Age
                                      }
                                      : null

            })
                .OrderByDescending(i => i.Id)
                .Skip(filter.Offset ?? 0)
                .Take(filter.Limit ?? 10)
                .ToList();

            return CustomApiResponses<GetDtosWithCount<List<ItemGetDto>>>.SuccesResult(new GetDtosWithCount<List<ItemGetDto>>
            {
                Data = itemDtos,
                Count = filteredItems.Count()
            });
        }

        public List<Item> FilterData(ItemFilterDto filter, IQueryable<Item> items)
        {
            if (filter.Id != null)
            {
                items = items.Where(i => i.Id == filter.Id);
            }
            if (!filter.Type.IsNullOrEmpty())
            {
                items = items.Where(i => i.Type.Contains(filter.Type));
            }
            if (filter.Cost != null)
            {
                items = items.Where(i => i.Cost == filter.Cost);
            }
            if (filter.SellerId != null)
            {
                items = items.Where(i => i.Seller.Id == filter.SellerId);
            }
            if (filter.BuyerId != null)
            {
                items = items.Where(i => i.Buyer.Id == filter.BuyerId);
            }
            return items.ToList();
        }

        public async Task<CustomApiResponses<ItemGetDto>> CreateItemAsync(ItemPostDto input, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                Type = input.Type,
                Cost = (int)input.Cost,
                BuyerId = input.BuyerId,
                SellerId = input.SellerId
            };


            await _repository.ItemRepository.CreateItemAsync(item, cancellationToken);
            await _repository.ItemRepository.SaveChangesAsync(cancellationToken);

            var itemQueryable = await _repository.ItemRepository.GetItemsAsync(cancellationToken);
            var fetchedItem = await itemQueryable
                                     .Include(i => i.Buyer)
                                     .Include(i => i.Seller)
                                    .FirstOrDefaultAsync(i => i.Id == item.Id, cancellationToken);

            if (fetchedItem == null)
            {
                throw new CustomExceptions.NotFoundException("Item not found");

            }

            var itemDto = new ItemGetDto
            {
                Id = fetchedItem.Id,
                Type = fetchedItem.Type,
                Cost = fetchedItem.Cost,
                Buyer = fetchedItem.Buyer != null
                                      ? new BuyerOnlyDto
                                      {
                                          Id = fetchedItem.Buyer.Id,
                                          Name = fetchedItem.Buyer.Name,
                                          Surname = fetchedItem.Buyer.Surname,
                                          Age = fetchedItem.Buyer.Age
                                      }
                                      : null,
                Seller = fetchedItem.Seller != null
                                      ? new SellerOnlyDto
                                      {
                                          Id = fetchedItem.Seller.Id,
                                          Name = fetchedItem.Seller.Name,
                                          Surname = fetchedItem.Seller.Surname,
                                          Age = fetchedItem.Seller.Age
                                      }
                                      : null

            };

            return CustomApiResponses<ItemGetDto>.SuccesResult(itemDto);
        }

        public async Task<CustomApiResponses<string>> UpdateItemAsync(ItemPutDto input, CancellationToken cancellationToken)
        {
            var itemQueryable = await _repository.ItemRepository.GetItemsAsync(cancellationToken);
            var item = await itemQueryable.AsQueryable()
                                        .Include(i => i.Buyer)
                                        .Include(i => i.Seller)
                                        .Where(b => b.Id == input.Id)
                                        .FirstOrDefaultAsync(cancellationToken);
            item.Id = input.Id.HasValue ? (int)input.Id.Value : item.Id;
            if(input.Type != null)
            {
                item.Type = input.Type;
            }
            else
            {
                item.Type = item.Type;
            }
            item.Cost = input.Cost.HasValue ? (int)input.Cost.Value : item.Cost;


            await _repository.ItemRepository.UpdateItemAsync(item, cancellationToken);

            if (input.SellerId != null)
            {
                item.Seller.Id = (int)input.SellerId;
                
            }
            if (input.BuyerId != null)
            {
                item.Buyer.Id = (int)input.BuyerId;
            }

            if (item == null)
            {
                throw new CustomExceptions.NotFoundException("Item not found");
            }

            await _repository.ItemRepository.SaveChangesAsync(cancellationToken);

            return CustomApiResponses<string>.SuccesResult("Item changed successfully");
        }

        public async Task<CustomApiResponses<string>> DeleteItemAsync(int itemId, CancellationToken cancellationToken)
        {
            var item = await _repository.ItemRepository.GetItemByIdAsync(itemId, cancellationToken);

            if (item == null)
            {
                throw new CustomExceptions.NotFoundException("item not found on that Id");
            }

            if (await _repository.ItemRepository.DeleteItemAsync(itemId, cancellationToken))
            {
                await _repository.ItemRepository.SaveChangesAsync(cancellationToken);
                return CustomApiResponses<string>.SuccesResult("Item deleted successfully");
            }
            throw new Exception();
        }
    }
}
