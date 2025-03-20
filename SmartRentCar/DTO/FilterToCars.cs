namespace SmartRentCar.DTO
{
    public class FilterToCars
    {
        public decimal? CostMin { get; set; }
        public decimal? CostMax { get; set; }
        public decimal? DepositMin { get; set; }
        public decimal? DepositMax { get;set; }
        public List<int>? CarBrands {  get; set; }
        public List<int>?CarClasses { get; set; }
        public int? CarTransmission { get; set; }
        public int? CarFuel { get; set; }
    }
}
