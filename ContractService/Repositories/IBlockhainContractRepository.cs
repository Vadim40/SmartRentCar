using ContractService.Models;

namespace ContractService.Repositories
{
    public interface IBlockhainContractRepository
    {
        public Task<List<BlockchainContract>> GetBlockchainContractsByStatus(int contractStatusId);

        public Task<BlockchainContract> GetBlockchainContractById( int contractId);
        public Task<int> SaveBlockchainContract(BlockchainContract blockchainContract);

        public Task<List<ContractStatus>> GetAllContractStatuses();
    }
}
