using SmartRentCar.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRentCar.DTO
{
    public class CarImageDTO
    {
        public int CarImageId { get; set; }

        public byte[] ImageData { get; set; }

    }
}
