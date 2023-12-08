using CarMarketApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMarketApi.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<User> Cars { get; set; }
        public DbSet<User> Sellers { get; set; }
        public DbSet<User> PrivateInformations { get; set; }
        public DbSet<User> SellerUserJoin { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SellersUsersJoin>()
                .HasKey(n => new { n.UserId, n.SellerId });
            modelBuilder.Entity<SellersUsersJoin>()
                .HasOne(u => u.User)
                .WithMany(su => su.SellersUsers)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<SellersUsersJoin>()
                .HasOne(s => s.Seller)
                .WithMany(su => su.SellersUsers)
                .HasForeignKey(s => s.SellerId);

            modelBuilder.Entity<User>()
                .HasOne(pi => pi.PrivateInformation)
                .WithOne(pi => pi.User)
                .HasForeignKey<PrivateInformation>(pi => pi.UserId);

            modelBuilder.Entity<Seller>()
                .HasOne(pi => pi.PrivateInformation)
                .WithOne(pi => pi.Seller)
                .HasForeignKey<PrivateInformation>(pi => pi.SellerId);



            base.OnModelCreating(modelBuilder); 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

    }
}
