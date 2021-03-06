using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingProject.Data.Models;
using ShoppingProject.Models;

namespace ShoppingProject.Data
{
    public class ShoppingDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<User> User{ get; set; }
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasOne(u => u.Wishlist)
                .WithOne(u => u.User)
                .HasForeignKey<Wishlist>(x => x.UserId);

            modelBuilder.Entity<User>()
               .HasOne(u => u.ShoppingCart)
               .WithOne(u => u.User)
               .HasForeignKey<ShoppingCart>(x => x.UserId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Wishlists)
                .WithMany(w => w.Products);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ShoppingCarts)
                .WithMany(c => c.Products);


            base.OnModelCreating(modelBuilder);

        }

        
    }
}