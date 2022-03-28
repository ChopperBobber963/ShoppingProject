using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Data.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public Wishlist Wishlist { get; set; } = new Wishlist();

        public ShoppingCart ShoppingCart { get; set; }
    }
}
