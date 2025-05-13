using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractService.Models
{
    public class DisputeMessage {
        [Key]
        [ForeignKey("DepositDispute")]
        public int DepositDisputeId {get; set;}
        public decimal DepositWithheld {get; set;}
        public string WithheldReason {get; set;}

        public DepositDispute DepositDispute {get; set;}
    }
}