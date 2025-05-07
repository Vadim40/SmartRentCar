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
        public DepositDisputeServiceImpl (IMapper mapper, IDepositDisputeRepository depositDisputeRepository)
        {
            _mapper = mapper;
            _depositDisputeRepository = depositDisputeRepository;
        }
        public async Task<DepositDisputeDTO> GetDepositDispute(int depositDisputeId)
        {
            var depositDispute = await _depositDisputeRepository.GetDepositDispute(depositDisputeId);
            return _mapper.Map<DepositDisputeDTO>(depositDispute);
        }

        public async Task<DepositDisputeDTO> GetDepositDisputeByRentalId(int rentalId)
        {
            var depositDispute = await _depositDisputeRepository.GetDepositDisputeByRenalId(rentalId);
            return _mapper.Map<DepositDisputeDTO>(depositDispute);
        }

        public async Task UpdateDispute(DisputeUpdateDTO disputeUpdate)
        {
            int status = DetermineDisputeStatus(disputeUpdate.Deposit, disputeUpdate.DepositWithheld);
            var dispute = new DepositDispute
            {
                DepositDisputeId = disputeUpdate.DepositDisputeId,
                Deposit = disputeUpdate.Deposit,
                DepositWithheld = disputeUpdate.DepositWithheld,
                DisputeStatusId = status
            };
            await _depositDisputeRepository.UpdateDispute(dispute);

        }

        private int  DetermineDisputeStatus(decimal deposit, decimal depositWithheld)
        {
            if (depositWithheld == 0) return (int)DepositDisputeStatus.DepositRefunded;
            if (depositWithheld == deposit) return (int) DepositDisputeStatus.DepositWithheld;
            else return (int) DepositDisputeStatus.PartialRefunded;

        }
    }
}