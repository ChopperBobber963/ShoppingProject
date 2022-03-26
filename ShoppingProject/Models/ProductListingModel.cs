namespace ShoppingProject.Models
{
    public class ProductListingModel
    {
        public const int ProductsPerPage = 3;

        public string ProductType { get; set; }

        public IEnumerable<string> ProductTypes { get; set; } = new List<string>();

        public string Search { get; set; }

        public int CurrentPage { get; set; } = 1;

        public IEnumerable<AllProductsForm> Products { get; set; } = new List<AllProductsForm>();
    }
}
