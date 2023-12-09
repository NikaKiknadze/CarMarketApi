using CarMarketApi.CustomResponses;
using CarMarketApi.Dtos.ItemDtos;
using CarMarketApi.Dtos;
using CarMarketApi.Entities;

namespace CarMarketApi.Service.ServiceAbstracts
{
    public interface IItemService
    {
        Task<CustomApiResponses<GetDtosWithCount<List<ItemGetDto>>>> GetItemsAsync(ItemFilterDto filter, CancellationToken cancellationToken);
        List<Item> FilterData(ItemFilterDto filter, IQueryable<Item> items);
        Task<CustomApiResponses<ItemGetDto>> CreateItemAsync(ItemPostDto input, CancellationToken cancellationToken);
        Task<CustomApiResponses<string>> UpdateItemAsync(ItemPutDto input, CancellationToken cancellationToken);
        Task<CustomApiResponses<string>> DeleteItemAsync(int itemId, CancellationToken cancellationToken);
    }
}
