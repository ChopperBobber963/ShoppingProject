using ShoppingProject.Data.Models;

namespace ShoppingProject.Models
{
    public class StoresListingModel
    {
        public ICollection<StoreDisplay> StoresDisplays { get; set; } = new List<StoreDisplay>();
    }
}
