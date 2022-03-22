using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingProject.Data.Models;

namespace ShoppingProject.Data
{
    public class ShoppingDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            : base(options)
        {
        }
    }
}