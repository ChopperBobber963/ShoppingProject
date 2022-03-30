using ShoppingProject.Data.Models;

namespace ShoppingProject.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public Wishlist Wishlist { get; set; } = new Wishlist();
    }
}
