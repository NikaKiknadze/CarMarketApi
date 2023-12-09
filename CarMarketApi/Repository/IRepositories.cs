using CarMarketApi.Repository.RepositoryAbstracts;

namespace CarMarketApi.Repository
{
    public interface IRepositories
    {
        IBuyerRepository BuyerRepository { get; }
        ISellerRepository SellerRepository { get; }
        IItemRepository ItemRepository { get; }
    }
}
