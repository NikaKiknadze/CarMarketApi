using CarMarketApi.Data;
using CarMarketApi.Entities;
using CarMarketApi.Repository.RepositoryAbstracts;
using Microsoft.EntityFrameworkCore;

namespace CarMarketApi.Repository.AllRepositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly Context _context;
        public SellerRepository(Context context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Seller>GetSellerByIdAsync(int sellerId, CancellationToken cancellationToken)
        {
            return await _context.Sellers
                                 .Include(s => s.SellerPersonalInformation)
                                 .Include(s => s.SellersBuyers)
                                 .Include(s => s.Items)
                                 .FirstOrDefaultAsync(s => s.Id == sellerId, cancellationToken);
        }

        public async Task<IQueryable<Seller>>GetSellersRelatedDataAsync(CancellationToken cancellationToken)
        {
            var sellers = await _context.Sellers
                                        .Include(s => s.Items)
                                        .Include(s => s.SellerPersonalInformation)
                                        .Include(s => s.SellersBuyers)
                                                .ThenInclude(sb => sb.Buyer)
                                        .ToListAsync(cancellationToken);
            return sellers.AsQueryable();
        }

        public async Task<IQueryable<Seller>>GetSellersAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _context.Sellers.AsQueryable(), cancellationToken);
        }

        public async Task<Seller>CreateSellerAsync(Seller seller, CancellationToken cancellationToken)
        {
            await _context.Sellers.AddAsync(seller, cancellationToken);
            return seller;
        }

        public async Task<bool>UpdateSellerAsync(Seller updatedSeller, CancellationToken cancellationToken)
        {
            var existingSeller = await _context.Sellers.FirstOrDefaultAsync(s => s.Id == updatedSeller.Id, cancellationToken);
            if(existingSeller == null)
            {
                return false;
            }

            existingSeller.Name = updatedSeller.Name;
            existingSeller.Surname = updatedSeller.Surname;
            existingSeller.Age = updatedSeller.Age;

            return true;
        }

        public async Task<bool>DeleteSellerAsync(int sellerId, CancellationToken cancellationToken)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Id == sellerId, cancellationToken);
            if(seller == null)
            {
                return false;
            }
            _context.Sellers.Remove(seller);
            return true;
        }

        public async Task<bool>DeleteSellersBuyersAsync(int sellerId, CancellationToken cancellationToken)
        {
            var sellersBuyers = await _context.SellersBuyersJoin
                                              .Where(s => s.SellerId == sellerId)
                                              .ToListAsync(cancellationToken);
            if (sellersBuyers == null)
            {
                return false;
            }

            _context.SellersBuyersJoin.RemoveRange(sellersBuyers);
            return true;
        }
    }
}
