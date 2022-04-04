using ShoppingProject.Data.Models;

namespace ShoppingProject.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public virtual Wishlist Wishlist { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }  

        public UserViewModel(int id, Wishlist wishlist)
        {
            Id = id;
            Wishlist = wishlist;
        }
    }
}
