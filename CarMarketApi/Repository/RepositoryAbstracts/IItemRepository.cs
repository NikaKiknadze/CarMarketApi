using CarMarketApi.Entities;

namespace CarMarketApi.Repository.RepositoryAbstracts
{
    public interface IItemRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<Item> GetItemByIdAsync(int itemId, CancellationToken cancellationToken);
        Task<IQueryable<Item>> GetItemsRelatedDataAsync(CancellationToken cancellationToken);
        Task<IQueryable<Item>> GetItemsAsync(CancellationToken cancellationToken);
        Task<Item> CreateItemAsync(Item item, CancellationToken cancellationToken);
        Task<bool> UpdateItemAsync(Item updatedItem, CancellationToken cancellationToken);
        Task<bool> DeleteItemAsync(int itemId, CancellationToken cancellationToken);
    }
}
