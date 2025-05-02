
using ContractService.Models;

namespace ContractService.Repositories
{
    public interface IDepositDisputeRepository
    {
        public Task<DepositDispute> GetDepositDispute( int depositDisputeId);
        public Task UpdateDisputeStatus(int depositDisputeId, int disputeStatusId);
    }
}