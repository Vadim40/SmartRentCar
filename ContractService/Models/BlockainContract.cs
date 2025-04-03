
namespace ContractService.Models
{
    public class BlockchainContract
    {
        public int BlockatinContractId { get; set; }
        public int ContractId {  get; set; }
        public int ContractStarusId { get; set; }
        
        public string TransactionHash { get; set; }
        public string ContractAddress { get; set; }
        public Decimal GasFee { get; set; }
    }
}