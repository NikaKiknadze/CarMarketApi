using CarMarketApi.CustomResponses;
using CarMarketApi.Dtos.SellerDtos;
using CarMarketApi.Dtos;
using CarMarketApi.Entities;

namespace CarMarketApi.Service.ServiceAbstracts
{
    public interface ISellerService
    {
        Task<CustomApiResponses<GetDtosWithCount<List<SellerGetDto>>>> GetSellersAsync(SellerFilterDto filter, CancellationToken cancellationToken);
        List<Seller> FilterData(SellerFilterDto filter, IQueryable<Seller> sellers);
        Task<CustomApiResponses<SellerGetDto>> CreateSellerAsync(SellerPostDto input, CancellationToken cancellationToken);
        Task<CustomApiResponses<string>> UpdateSellerAsync(SellerPutDto input, CancellationToken cancellationToken);
        Task<CustomApiResponses<string>> DeleteSellerAsync(int sellerId, CancellationToken cancellationToken);
    }
}
