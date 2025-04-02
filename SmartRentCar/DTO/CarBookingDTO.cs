using System.ComponentModel.DataAnnotations;

namespace SmartRentCar.DTO
{
    public class CarBookingDTO
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
