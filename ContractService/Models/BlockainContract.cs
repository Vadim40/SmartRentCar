
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractService.Models
{
    public class BlockchainContract
    {
        [Key]
        public int BlockchainContractId { get; set; }
        public int ContractId {  get; set; }
        [ForeignKey("ContractStatus")]
        public int ContractStatusId { get; set; }
        
        public string TransactionHash { get; set; }
        public string ContractAddress { get; set; }
        public Decimal GasFee { get; set; }

        public ContractStatus ContractStatus { get; set; }
    }
}