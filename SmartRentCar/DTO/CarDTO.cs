using SmartRentCar.Models.CarInfo;
using SmartRentCar.Models;


namespace SmartRentCar.DTO
{
    public class CarDTO
    {
        public int CarId { get; set; }

 
        public int ClassName { get; set; }

        public int FuelTypeName { get; set; }
        public int TransmissionTypeName{ get; set; }

        public decimal CostPerDay { get; set; }
        public decimal DepositAmount { get; set; }
        public string CarName { get; set; }

    }
}
