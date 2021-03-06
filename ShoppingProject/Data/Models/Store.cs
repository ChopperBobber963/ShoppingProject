using System.ComponentModel.DataAnnotations;

namespace ShoppingProject.Data.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string WorkHours { get; set; }

        public string ClosingHours { get; set; }

        public string Details { get; set; }
    }
}
