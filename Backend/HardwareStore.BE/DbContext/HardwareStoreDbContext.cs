using Microsoft.EntityFrameworkCore;
using HardwareStore.BE.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HardwareStore.BE.DbContext
{
    //public class HardwareStoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    public class HardwareStoreDbContext : IdentityDbContext<User>
    {
        public HardwareStoreDbContext(DbContextOptions<HardwareStoreDbContext> options) : base(options) { }

        // MY TABLES
        public DbSet<Article> Articles { get; set; } = null; // Articles
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(
                new Article(1, "Corsair RAM 16GB", "Best RAM in the market so far", "RAM", 20.50),
                new Article(2, "Corsair Vengeance LPX Black 8GB 3600 Mhz DDR4", "Best RAM memory with a good quality-price ratio", "RAM", 40),
                new Article(3, "MSI Ventus XS GeForce GTX 1660TI", "The most seeked graphics card to play your beloved games", "GPU", 250),
                new Article(4, "Amd Ryzen 9 5900x 4.8ghz", "Best processor in the market according to various reviews", "CPU", 180),
                new Article(5, "Asus Va24ehe 23.8 1920x1080", "Full HD monitor on a budget", "Monitor", 255)
            );

            //modelBuilder.Entity<User>().HasMany(e => e.PaymentMethods).WithOne(e => e.CardHolder).HasForeignKey(e => e.UserId).HasPrincipalKey(e => e.Id);
            modelBuilder.HasDefaultSchema("identity");

            base.OnModelCreating(modelBuilder);
        }
    }
}
