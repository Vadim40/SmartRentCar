namespace ContractService.DTOs
{
    public class DisputeMessageDTO{
      
        public int DepositDisputeId {get; set;}
        public decimal DepositWithheld {get; set;}
        public string WithheldReason {get; set;}

    }
}