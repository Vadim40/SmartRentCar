using ContractService.DTOs;

namespace ContractService.Services
{
    public interface IDepositDisputeService
    {
        public Task<DepositDisputeDTO> GetDepositDispute (int depositDisputeId);
        public Task UpdateDisputeStatus(int depositDisputeId, int disputeStatusId);
        
    }
}