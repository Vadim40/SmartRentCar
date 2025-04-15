namespace SmartRentCar.DTO
{
    public class RentContractCreate
    {
        public int CarId { get; set; }
        public DateTime StardDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal deposit {  get; set; }
    }
}
