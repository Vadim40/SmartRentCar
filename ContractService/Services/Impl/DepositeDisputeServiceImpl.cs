using AutoMapper;
using ContractService.DTOs;
using ContractService.Models;
using ContractService.Repositories;

namespace ContractService.Services.Impl
{
    public class DepositDisputeServiceImpl : IDepositDisputeService
    {
        private readonly IMapper _mapper;
        private readonly IDepositDisputeRepository _depositDisputeRepository;
        public DepositDisputeServiceImpl(IMapper mapper, IDepositDisputeRepository depositDisputeRepository)
        {
            _mapper = mapper;
            _depositDisputeRepository = depositDisputeRepository;
        }

        public async Task CreateDisputeMessage(DisputeMessageDTO disputeMessageDTO)
        {
           var disputeMessage = _mapper.Map<DisputeMessage>(disputeMessageDTO);
           await _depositDisputeRepository.CreateDisputeMessage(disputeMessage);
        }

        public async Task<DepositDisputeDTO> GetDepositDispute(int depositDisputeId)
        {
            var depositDispute = await _depositDisputeRepository.GetDepositDispute(depositDisputeId);
            return _mapper.Map<DepositDisputeDTO>(depositDispute);
        }

        public async Task<DepositDisputeDTO> GetDepositDisputeByRentalId(int rentalId)
        {
            var depositDispute = await _depositDisputeRepository.GetDepositDisputeByRentalId(rentalId);
            return _mapper.Map<DepositDisputeDTO>(depositDispute);
        }

        public async Task<DisputeMessageDTO> GetDisputeMessage(int depositDisputeId)
        {
            var disputeMessage = await _depositDisputeRepository.GetDisputeMessage(depositDisputeId);
            if (disputeMessage == null)
            return null;
            
            return _mapper.Map<DisputeMessageDTO>(disputeMessage);
        }

        public async Task UpdateDispute(DisputeUpdateDTO disputeUpdate)
        {
            int status = DetermineDisputeStatus(disputeUpdate.Deposit, disputeUpdate.DepositWithheld);
              
            var dispute = _mapper.Map<DepositDispute>(disputeUpdate);
            dispute.DisputeStatusId =status;
            await _depositDisputeRepository.UpdateDispute(dispute);

        }

        public async Task UpdateDisputeMessage(DisputeMessageDTO disputeMessageDTO)
        {
           var disputeMessage = _mapper.Map<DisputeMessage>(disputeMessageDTO);
           await _depositDisputeRepository.UpdateDisputeMessage(disputeMessage);
        }

        private int DetermineDisputeStatus(decimal deposit, decimal depositWithheld)
        {
            if (depositWithheld == 0) return (int)DepositDisputeStatus.DepositRefunded;
            if (depositWithheld == deposit) return (int)DepositDisputeStatus.DepositWithheld;
            else return (int)DepositDisputeStatus.PartialRefunded;

        }
    }
}