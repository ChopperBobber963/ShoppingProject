using Microsoft.EntityFrameworkCore;
using ShoppingProject.Data;
using ShoppingProject.Data.Models;
using ShoppingProject.Models;

namespace ShoppingProject.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingDbContext _dbContext;

        public ProductService(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Products.Remove(result);
            await _dbContext.SaveChangesAsync();
        }

        public AllProductsForm Details(int id)
        {
            return _dbContext.Products
                           .Where(p => p.Id == id)
                           .Select(p => new AllProductsForm
                           {
                               Id = p.Id,
                               Name = p.Name,
                               ImageURL = p.ImageURL,
                               Price = p.Price,
                               Description = p.Description,
                               ProductType = p.ProductType
                           })
                           .FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var result = await _dbContext.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetById(int id)
        {
            var result = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task<Product> Update(Product newProduct)
        {
            _dbContext.Update(newProduct);
            await _dbContext.SaveChangesAsync();
            return newProduct;
        }
    }
}
