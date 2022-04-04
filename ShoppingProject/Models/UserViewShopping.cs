using ShoppingProject.Data.Models;

namespace ShoppingProject.Models
{
    public class UserViewShopping
    {
        public int Id { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public UserViewShopping(int id, ShoppingCart shoppingCart)
        {
            Id = id;
            ShoppingCart = shoppingCart;

        }

    }
}
