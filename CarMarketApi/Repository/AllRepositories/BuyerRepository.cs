using CarMarketApi.Data;
using CarMarketApi.Entities;
using CarMarketApi.Repository.RepositoryAbstracts;
using Microsoft.EntityFrameworkCore;

namespace CarMarketApi.Repository.AllRepositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly Context _context;
        public BuyerRepository(Context context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Buyer>GetBuyerByIdAsync(int buyerId, CancellationToken cancellationToken)
        {
            return await _context.Buyers
                .Include(b => b.Items)
                .Include(b => b.PersonalInformation)
                .Include(b => b.SellersBuyers)
                .FirstOrDefaultAsync(b => b.Id == buyerId, cancellationToken);
        }
        
        public async Task<IQueryable<Buyer>>GetBuyersWithRelatedDataAsync(CancellationToken cancellationToken)
        {
            var buyer = await _context.Buyers
                                      .Include(b => b.Items)
                                      .Include(b => b.PersonalInformation)
                                      .Include(b => b.SellersBuyers)
                                            .ThenInclude(sb => sb.Seller)
                                      .ToListAsync(cancellationToken);
            return buyer.AsQueryable();
        }
        
        public async Task<IQueryable<Buyer>>GetBuyersAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _context.Buyers.AsQueryable(), cancellationToken);
        }

        public async Task<Buyer>CreateBuyerAsync(Buyer buyer, CancellationToken cancellationToken)
        {
            await _context.Buyers.AddAsync(buyer, cancellationToken);
            return buyer;
        }

        public async Task<bool>UpdateBuyerAsync(Buyer updatedBuyer, CancellationToken cancellationToken)
        {
            var existingBuyer = await _context.Buyers.FirstOrDefaultAsync(b => b.Id == updatedBuyer.Id, cancellationToken);

            if (existingBuyer == null)
            {
                return false;
            }

            existingBuyer.Name = updatedBuyer.Name;
            existingBuyer.Surname = updatedBuyer.Surname;
            existingBuyer.Age = updatedBuyer.Age;

            return true;
        }

        public async Task<bool>DeleteBuyerAsync(int buyerId, CancellationToken cancellationToken)
        {
            var buyer = await _context.Buyers.FirstOrDefaultAsync(b => b.Id ==  buyerId, cancellationToken);

            if(buyer == null)
            {
                return false;
            }

            _context.Buyers.Remove(buyer);
            return true;
        }

        public async Task<bool>DeleteSellersBuyersAsync(int buyerId, CancellationToken cancellationToken)
        {
            var sellersBuyers = await _context.SellersBuyersJoin
                                              .Where(b => b.BuyerId == buyerId)
                                              .ToListAsync(cancellationToken);

            if(sellersBuyers == null)
            {
                return false;
            }

            _context.SellersBuyersJoin.RemoveRange(sellersBuyers);
            return true;

        }

    }
}
