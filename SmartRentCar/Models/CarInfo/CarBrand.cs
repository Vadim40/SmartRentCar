using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.Models.CarInfo
{
    public class CarBrand
    {
        [Key]
        public int BrandId { get; set; }
        public string Name { get; set; }
    }
}
