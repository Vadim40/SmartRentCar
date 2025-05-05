using AutoMapper;
using ContractService.DTOs;
using ContractService.Repositories;

namespace ContractService.Services.Impl
{
    public class DepositDisputeImpl : IDepositDisputeService
    {
        private readonly IMapper _mapper;
        private readonly IDepositDisputeRepository _depositDisputeRepository;
        public DepositDisputeImpl (IMapper mapper, IDepositDisputeRepository depositDisputeRepository)
        {
            _mapper = mapper;
            _depositDisputeRepository = depositDisputeRepository;
        }
        public async Task<DepositDisputeDTO> GetDepositDispute(int depositDisputeId)
        {
            var depositDispute = await _depositDisputeRepository.GetDepositDispute(depositDisputeId);
            return _mapper.Map<DepositDisputeDTO>(depositDispute);
        }

        public async Task UpdateDisputeStatus(int depositDisputeId, int disputeStatusId)
        {
            await _depositDisputeRepository.UpdateDisputeStatus(depositDisputeId, disputeStatusId);
        }
    }
}