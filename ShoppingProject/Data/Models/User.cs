using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Data.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public virtual Wishlist Wishlist { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
