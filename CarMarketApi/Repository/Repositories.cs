using CarMarketApi.Repository.AllRepositories;
using CarMarketApi.Repository.RepositoryAbstracts;

namespace CarMarketApi.Repository
{
    public class Repositories : IRepositories
    {
        public Repositories(IBuyerRepository buyerRepository,
                            ISellerRepository sellerRepository,
                            IItemRepository itemRepository)
        {
            BuyerRepository = buyerRepository;
            SellerRepository = sellerRepository;
            ItemRepository = itemRepository;
        }
        public IBuyerRepository BuyerRepository { get; }
        public ISellerRepository SellerRepository { get; }
        public IItemRepository ItemRepository { get; }
    }
}
