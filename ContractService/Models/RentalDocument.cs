using System.ComponentModel.DataAnnotations.Schema;
using SmartRentCar.Models;

namespace ContractService.Models
{
    public class RentalDocument
    {

        public int RentalDocumentId { get; set; }
        [ForeignKey("DepositDispute")]
        public int DepositDisputeId { get; set; }

        [ForeignKey("Rental")]
        public int RentalId {get; set;}

        public byte[] FileData { get; set; }
        public string FileName { get; set; }

        public DepositDispute DepositDispute { get; set; }
        public Rental Rental {get; set;}
    }
}