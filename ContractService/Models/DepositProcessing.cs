namespace ContractService.Models
{
    public class DepositProcessing
    {
       public int DepositProcessingId { get; set; }
        public int BlockainContractId { get; set; }
       public int ProcessingStatusId { get; set; }
       public string EvidenceDocument { get; set; }
    }
}
