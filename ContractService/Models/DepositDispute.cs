using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractService.Models
{
    public class DepositDispute
    {
        [Key]
       public int DepositDisputeId { get; set; }
        [ForeignKey("Contract")]
        public int ContractId { get; set; }
        [ForeignKey("DisputeStatus")]
       public int DisputeStatusId { get; set; }

        public Contract Contract { get; set; }
        public DisputeStatus DisputeStatus { get; set; }
    }
}
