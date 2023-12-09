using CarMarketApi.Data;
using CarMarketApi.Entities;
using CarMarketApi.Repository.RepositoryAbstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarMarketApi.Repository.AllRepositories
{
    public class ItemRepository :IItemRepository
    {
        private readonly Context _context;
        public ItemRepository(Context context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Item> GetItemByIdAsync(int itemId, CancellationToken cancellationToken)
        {
            return await _context.Items
                                 .Include(i => i.Seller)
                                 .Include(i => i.Buyer)
                                 .FirstOrDefaultAsync(i => i.Id == itemId, cancellationToken);

        }

        public async Task<IQueryable<Item>> GetItemsRelatedDataAsync(CancellationToken cancellationToken)
        {
            var items = await _context.Items
                            .Include(i => i.Seller)
                            .Include(i => i.Buyer)
                            .ToListAsync(cancellationToken);
            return items.AsQueryable();
        }

        public async Task<IQueryable<Item>> GetItemsAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _context.Items.AsQueryable(), cancellationToken);
        }

        public async Task<Item> CreateItemAsync(Item item, CancellationToken cancellationToken)
        {
            await _context.Items.AddAsync(item, cancellationToken);
            return item;
        }

        public async Task<bool> UpdateItemAsync(Item updatedItem, CancellationToken cancellationToken)
        {
            var existingItem = await _context.Items.FirstOrDefaultAsync(i => i.Id == updatedItem.Id, cancellationToken);
            if (existingItem == null)
            {
                throw new CustomExceptions.NotFoundException("Items not found");
            }
            existingItem.Type = updatedItem.Type;
            existingItem.Cost = updatedItem.Cost;

            return true;
        }

        public async Task<bool> DeleteItemAsync(int itemId, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId, cancellationToken);
            if (item == null)
            {
                throw new CustomExceptions.NotFoundException("Items not found");
            }
            _context.Items.Remove(item);
            
            return true;
        }
    }
}
