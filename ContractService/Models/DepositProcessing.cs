using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractService.Models
{
    public class DepositProcessing
    {
        [Key]
       public int DepositProcessingId { get; set; }
        [ForeignKey("BlockchainContract")]
        public int BlockainContractId { get; set; }
        [ForeignKey("ProcessingStatus")]
       public int ProcessingStatusId { get; set; }
       public string EvidenceDocument { get; set; }

        public BlockchainContract BlockchainContract { get; set; }
        public ProcessingStatus ProcessingStatus { get; set; }
    }
}
