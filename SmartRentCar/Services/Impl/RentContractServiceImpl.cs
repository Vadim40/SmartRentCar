using AutoMapper;
using SmartRentCar.Config;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Repositories;

namespace SmartRentCar.Services.Impl
{
    public class RentContractServiceImpl : IRentContractService
    {
        private readonly IRentContractRepository _rentContractRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public RentContractServiceImpl(IRentContractRepository rentContractRepository, IMapper mapper, ApplicationContext context)
        {

            _rentContractRepository = rentContractRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task DeleteRentContractById(int contractId)
        {
            try
            {
                await _rentContractRepository.DeleteRentContractById(contractId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RentContractDTO>> GetRentContractsByStatus(int userId, int statusId)
        {
            try
            {
                var rentContracts = await _rentContractRepository.GetRentContractsByStatus(userId, statusId);
                return _mapper.Map<List<RentContractDTO>>(rentContracts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> SaveRentContract(RentContractDTO contractDTO)
        {
            try
            {
                bool isCarInRent = _context.RentContracts.
                                    Where(r => contractDTO.StartDate<=r.EndDate
                                    && contractDTO.EndDate>= r.StartDate)
                                    .Any();
                if (isCarInRent)
                    throw new Exception("This car is already in rent on the days you chose.");

                var rentContract = _mapper.Map<RentContract>(contractDTO);
                return _rentContractRepository.SaveRentContract(rentContract);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateRentContract(RentContractUpateDTO contractDTO)
        {
            try
            {
                var rentContract = _mapper.Map<RentContract>(contractDTO);
                await _rentContractRepository.UpdateRentContract(rentContract);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
