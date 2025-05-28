using ContractService.DTOs;

namespace ContractService.Services
{
    public interface IDepositDisputeService
    {
        public Task<DepositDisputeDTO> GetDepositDispute (int depositDisputeId);
        public Task UpdateDispute(DisputeUpdateDTO disputeUpdate);
        
        public Task<DepositDisputeDTO> GetDepositDisputeByRentalId( int rentalId);
        public Task UpdateDisputeMessage(DisputeMessageDTO disputeMessage);
        public Task CreateDisputeMessage(DisputeMessageDTO disputeMessage);

        public Task<DisputeMessageDTO> GetDisputeMessage(int depositDisputeId);
    }
}