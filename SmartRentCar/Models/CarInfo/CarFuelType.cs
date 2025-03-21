using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.Models.CarInfo
{
    public class CarFuelType
    {
        [Key]
        public int CarFuelTypeId { get; set; }
        public string Name { get; set; }
    }
}
