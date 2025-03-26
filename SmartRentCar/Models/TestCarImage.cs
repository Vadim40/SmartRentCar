using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.Models
{
    public class TestCarImage
    {
        [Key]
        public int CarImageId { get; set; }
        public int CarId { get; set; }
        public byte[]? ImageData { get; set; }
    }

}
