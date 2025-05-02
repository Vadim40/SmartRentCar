using System.ComponentModel.DataAnnotations.Schema;

namespace ContractService.Models
{
    public class RentalDocument
    {

        public int RentalDocumentId { get; set; }
        [ForeignKey("DepositDispute")]
        public int DepositDisputeId { get; set; }

        public byte[] FileData { get; set; }
        public string FileName { get; set; }

        public DepositDispute DepositDispute { get; set; }
    }
}