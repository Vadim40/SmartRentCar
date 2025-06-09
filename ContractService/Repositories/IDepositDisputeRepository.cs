
using ContractService.Models;

namespace ContractService.Repositories
{
    public interface IDepositDisputeRepository
    {
        public Task<DepositDispute> GetDepositDispute( int depositDisputeId);
        public Task<DepositDispute> GetDepositDisputeByRentalId(int rentalId);
        public Task UpdateDispute(DepositDispute disputeUpdate);
        public Task UpdateDisputeMessage(DisputeMessage disputeMessage);
        public Task CreateDisputeMessage( DisputeMessage disputeMessage);
        public Task<DisputeMessage> GetDisputeMessage(int depositDisputeId);
    }
}