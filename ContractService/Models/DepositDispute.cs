using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractService.Models
{
    public class DepositDispute
    {
        [Key]
       public int DepositProcessingId { get; set; }
        [ForeignKey("BlockchainContract")]
        public int BlockainContractId { get; set; }
        [ForeignKey("ProcessingStatus")]
       public int ProcessingStatusId { get; set; }
       public string EvidenceDocument { get; set; }

        public Contract BlockchainContract { get; set; }
        public DisputeStatus ProcessingStatus { get; set; }
    }
}
