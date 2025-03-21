using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.Models.CarInfo
{
    public class CarClass
    {
        [Key]
        public int CarClassId { get; set; }
        public string Name { get; set; }

    }
}
