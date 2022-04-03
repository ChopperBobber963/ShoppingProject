using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingProject.Data.Models
{
    using static DataConstants;
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.Product.ProductNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(DataConstants.Product.ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public string ProductType { get; set; }

        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

    }
}
