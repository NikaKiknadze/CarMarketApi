using CarMarketApi.CustomResponses;
using CarMarketApi.Dtos.BuyerDtos;
using CarMarketApi.Dtos;
using CarMarketApi.Entities;

namespace CarMarketApi.Service.ServiceAbstracts
{
    public interface IBuyerService
    {
        Task<CustomApiResponses<GetDtosWithCount<List<BuyerGetDto>>>> GetBuyersAsync(BuyerFilterDto filter, CancellationToken cancellationToken);
        List<Buyer> FilterData(BuyerFilterDto filter, IQueryable<Buyer> buyers);
        Task<CustomApiResponses<BuyerGetDto>> CrreateBuyerAsync(BuyerPostDto input, CancellationToken cancellationToken);
        Task<CustomApiResponses<string>> UpdateBuyerAsync(BuyerPutDto input, CancellationToken cancellationToken);
        Task<CustomApiResponses<string>> DeleteBuyerAsync(int buyerId, CancellationToken cancellationToken);
    }
}
