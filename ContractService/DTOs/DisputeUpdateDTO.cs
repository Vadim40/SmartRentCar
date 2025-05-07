namespace ContractService.DTOs
{
    public class DisputeUpdateDTO
    {
        public int DepositDisputeId { get; set; }
        public decimal Deposit { get; set; }
        public decimal DepositWithheld { get; set; }
    }
}
