using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRentCar.Models.CarInfo
{
    public class CarImage
    {
        [Key]
        public int CarImageId { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
     
        public byte[] ImageData { get; set; }

        public Car Car { get; set; }
    }
}
