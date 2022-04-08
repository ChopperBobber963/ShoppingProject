using ShoppingProject.Data.Models;
using ShoppingProject.Models;

namespace ShoppingProject.Services
{
    public interface IProductService
    {
        // Cannot return null.
        Task <List<Product>> GetAll();

        Task<Product> GetById(int id);

        void Add(Product product);

        Task<Product> Update(Product newProduct);

        Task Delete(int id);

        AllProductsForm Details(int id);
    }
}
