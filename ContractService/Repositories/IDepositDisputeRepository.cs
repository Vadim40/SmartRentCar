
using ContractService.Models;

namespace ContractService.Repositories
{
    public interface IDepositDisputeRepository
    {
        public Task<DepositDispute> GetDepositDispute( int depositDisputeId);
        public Task<DepositDispute> GetDepositDisputeByRenalId(int rentalId);
        public Task UpdateDispute(DepositDispute disputeUpdate);
    }
}