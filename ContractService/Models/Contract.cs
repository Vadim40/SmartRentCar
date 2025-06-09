
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace ContractService.Models
// {
//     public class Contract
//     {
//         [Key]
//         public int ContractId { get; set; }
//         public int RentalId { get; set; }

//         [ForeignKey("DepositDispute")]
//         public int DepositDisputeId { get; set; }
//         public string ContractAddress { get; set; }

//         public DepositDispute DepositDispute { get; set; }
//     }
// }