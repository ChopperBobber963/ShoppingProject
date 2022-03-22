using System.ComponentModel.DataAnnotations;
using static ShoppingProject.Data.DataConstants;
namespace ShoppingProject.Models
{

    public class AddProductForm
    {
         
        [Required]
        [StringLength(ProductNameMaxLength,
            MinimumLength = 3,
            ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Name { get; set; }

        [StringLength(ProductDescriptionMaxLength, MinimumLength = 5,
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
