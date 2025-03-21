using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.Models.CarInfo
{
    public class CarTransmission
    {
        [Key]
        public int CarTransmissionId { get; set; }
        public string Name { get; set; }

    }
}
