using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Data.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
