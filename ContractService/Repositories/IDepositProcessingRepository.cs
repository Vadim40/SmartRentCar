using ContractService.Models;

namespace ContractService.Repositories
{
    public interface IDepositProcessingRepository
    {
        public Task<List<DepositProcessing>> GetDepositProcessingsByStatus(int processingStatusId);
    }
}
