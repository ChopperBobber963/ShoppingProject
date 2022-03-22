using ShoppingProject.Data.Models;
using ShoppingProject.Models;

namespace ShoppingProject.Services
{
    public interface IProductService
    {
        Task <IEnumerable<Product>> GetAll();

        Product GetById(int id);

        void Add(Product product);

        Product Update(int id, Product product);

        void Delete(int id);
    }
}
