using SmartRentCar.Models;

namespace SmartRentCar.DTO
{
    public class RentContractDTO
    {
      
        public int RentContractId { get; set; }
        public int CarId {get; set;}

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        public decimal TotalCost { get; set; }

        public decimal Deposit { get; set; }


        public DateTime CreatedAt { get; set; }


    }
}
