using CarMarketApi.Entities;

namespace CarMarketApi.Repository.RepositoryAbstracts
{
    public interface IBuyerRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<Buyer> GetBuyerByIdAsync(int buyerId, CancellationToken cancellationToken);
        Task<IQueryable<Buyer>> GetBuyersWithRelatedDataAsync(CancellationToken cancellationToken);
        Task<IQueryable<Buyer>> GetBuyersAsync(CancellationToken cancellationToken);
        Task<Buyer> CreateBuyerAsync(Buyer buyer, CancellationToken cancellationToken);
        Task<bool> UpdateBuyerAsync(Buyer updatedBuyer, CancellationToken cancellationToken);
        Task<bool> DeleteBuyerAsync(int buyerId, CancellationToken cancellationToken);
        Task<bool> DeleteSellersBuyersAsync(int buyerId, CancellationToken cancellationToken);
    }
}
