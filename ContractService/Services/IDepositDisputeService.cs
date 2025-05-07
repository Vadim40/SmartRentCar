using ContractService.DTOs;

namespace ContractService.Services
{
    public interface IDepositDisputeService
    {
        public Task<DepositDisputeDTO> GetDepositDispute (int depositDisputeId);
        public Task UpdateDispute(DisputeUpdateDTO disputeUpdate);
        
        public Task<DepositDisputeDTO> GetDepositDisputeByRentalId( int rentalId);
    }
}