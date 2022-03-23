namespace ShoppingProject.Models
{
    public class ProductListingModel
    {
        public IEnumerable<AllProductsForm> Products { get; set; } = new List<AllProductsForm>();
    }
}
