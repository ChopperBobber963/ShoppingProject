namespace ShoppingProject.Models
{
    public class ProductListingModel
    {
        public string Search { get; set; }

        public IEnumerable<AllProductsForm> Products { get; set; } = new List<AllProductsForm>();
    }
}
