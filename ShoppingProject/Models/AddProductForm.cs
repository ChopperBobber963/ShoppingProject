using ShoppingProject.Data;
using System.ComponentModel.DataAnnotations;
using static ShoppingProject.Data.DataConstants;
namespace ShoppingProject.Models
{

    public class AddProductForm
    {
        public int Id { get; set; }
         
        [Required]
        [StringLength(DataConstants.Product.ProductNameMaxLength,
            MinimumLength = 3,
            ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Name { get; set; }

        [StringLength(DataConstants.Product.ProductDescriptionMaxLength, MinimumLength = 5,
            ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue,ErrorMessage = "{0} must be between {2} and {1}")]
        public double Price { get; set; }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        public string ProductType { get; set; }
    }
}
