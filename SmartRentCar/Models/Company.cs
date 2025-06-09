using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public byte[]? ImageData { get; set; }
    }
}