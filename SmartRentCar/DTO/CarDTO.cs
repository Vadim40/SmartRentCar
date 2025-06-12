using SmartRentCar.Models.CarInfo;
using SmartRentCar.Models;


namespace SmartRentCar.DTO
{
    public class CarDTO
    {
        public int CarId { get; set; }

        public int CompanyId { get; set; }
        public string ClassName { get; set; }

        public string FuelTypeName { get; set; }
        public string TransmissionTypeName{ get; set; }

        public decimal CostPerDay { get; set; }
        public decimal DepositAmount { get; set; }
        public string CarName { get; set; }

    }
}
