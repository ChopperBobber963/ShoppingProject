using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Data.Models
{
    using static DataConstants;
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public string PictureURL { get; set; }

        public string ProductType { get; set; }
    }
}
