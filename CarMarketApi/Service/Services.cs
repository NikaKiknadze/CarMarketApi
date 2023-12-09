using CarMarketApi.Service.AllServices;
using CarMarketApi.Service.ServiceAbstracts;

namespace CarMarketApi.Service
{
    public class Services : IServices
    {
        public Services(IBuyerService buyerService,
                        IItemService itemService,
                        ISellerService sellerService)
        {
            BuyerService = buyerService;
            ItemService = itemService;
            SellerService = sellerService;
        }
        public IBuyerService BuyerService { get; }
        public IItemService ItemService { get; }
        public ISellerService SellerService { get; }
    }
}
