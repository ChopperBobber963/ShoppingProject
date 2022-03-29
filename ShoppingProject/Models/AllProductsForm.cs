using ShoppingProject.Data.Models;

namespace ShoppingProject.Models
{
    public class AllProductsForm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string ImageURL { get; set; }

        public string ProductType { get; set; }

        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
