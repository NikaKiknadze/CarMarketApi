using CarMarketApi.Data;
using CarMarketApi.Repository;
using CarMarketApi.Service.ServiceAbstracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarMarketApi.Service
{
    public interface IServices
    {
        IBuyerService BuyerService { get; }
        IItemService ItemService { get; }
        ISellerService SellerService { get; }
    }
}
