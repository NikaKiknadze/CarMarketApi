using CarMarketApi.Entities;

namespace CarMarketApi.Repository.RepositoryAbstracts
{
    public interface ISellerRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<Seller> GetSellerByIdAsync(int sellerId, CancellationToken cancellationToken);
        Task<IQueryable<Seller>> GetSellersRelatedDataAsync(CancellationToken cancellationToken);
        Task<IQueryable<Seller>> GetSellersAsync(CancellationToken cancellationToken);
        Task<Seller> CreateSellerAsync(Seller seller, CancellationToken cancellationToken);
        Task<bool> UpdateSellerAsync(Seller updatedSeller, CancellationToken cancellationToken);
        Task<bool> DeleteSellerAsync(int sellerId, CancellationToken cancellationToken);
        Task<bool> DeleteSellersBuyersAsync(int sellerId, CancellationToken cancellationToken);
    }
}
