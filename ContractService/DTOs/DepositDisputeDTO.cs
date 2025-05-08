

namespace ContractService.DTOs
{
    public class DepositDisputeDTO
    {
        public int DepositDisputeId { get; set; }
   
        public int ContractId { get; set; }
        public string DisputeStatusName { get; set; }
        public decimal Deposit {get; set;}
        public decimal? DepositWithheld {get; set;}

    }
}
