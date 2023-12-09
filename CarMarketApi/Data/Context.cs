using CarMarketApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMarketApi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<BuyerPersonalInformation> BuyersPersonalInformations { get; set; }
        public DbSet<SellerPersonalInformation> SellersPersonalInformations { get; set; }
        public DbSet<SellersBuyersJoin> SellersBuyersJoin { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellersBuyersJoin>()
                .HasKey(n => new { n.BuyerId, n.SellerId });
            modelBuilder.Entity<SellersBuyersJoin>()
                .HasOne(u => u.Buyer)
                .WithMany(su => su.SellersBuyers)
                .HasForeignKey(u => u.BuyerId);
            modelBuilder.Entity<SellersBuyersJoin>()
                .HasOne(s => s.Seller)
                .WithMany(su => su.SellersBuyers)
                .HasForeignKey(s => s.SellerId);

            base.OnModelCreating(modelBuilder); 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

    }
}
