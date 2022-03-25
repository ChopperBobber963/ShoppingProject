namespace ShoppingProject.Models
{
    public class ProductListingModel
    {
        public const int ProductsPerPage = 3;

        public string Search { get; set; }

        public int CurrentPage { get; set; } = 1;

        public IEnumerable<AllProductsForm> Products { get; set; } = new List<AllProductsForm>();
    }
}
