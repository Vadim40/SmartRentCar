using SmartRentCar.Models;

namespace SmartRentCar.DTO
{
    public class RentContractUpateDTO
    {
        public int RentContractId { get; set; }
        public int? СontractStatusId { get; set; }

        public string? ContractAddress {get; set;}
    }
}
