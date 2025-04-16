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
            await _rentContractRepository.DeleteRentContractById(contractId);
        }

        public async Task<List<RentContractDTO>> GetRentContractsCompleted()
        {
            //TODO  заменить
            var rentContracts = await _rentContractRepository.GetRentContracts(1);
            rentContracts = rentContracts.Where(r => r.ContractStatusId == (int)RentContractStatus.Completed
                                                && r.ContractStatusId == (int)RentContractStatus.Canceled).ToList();
            return _mapper.Map<List<RentContractDTO>>(rentContracts);
        }

        public async Task<List<RentContractDTO>> GetRentContractsActive()
        {
            var rentContracts = await _rentContractRepository.GetRentContracts(1);
            rentContracts = rentContracts.Where(r => r.ContractStatusId != (int)RentContractStatus.Completed
                                                 && r.ContractStatusId != (int)RentContractStatus.Canceled).ToList();
            return _mapper.Map<List<RentContractDTO>>(rentContracts);
        }

        public async Task<List<RentContractDTO>> GetRentContractsByStatus(int statusId)
        {
            // TODO заменить на реальный UserId
            var rentContracts = await _rentContractRepository.GetRentContractsByStatus(1, statusId);
            return _mapper.Map<List<RentContractDTO>>(rentContracts);
        }

        public async Task<int> SaveRentContract(RentContractCreateDTO contractDTO)
        {
            bool isCarInRent = await _rentContractRepository.IsCarInRent(contractDTO.CarId, contractDTO.StartDate, contractDTO.EndDate);

            if (isCarInRent)
            {
                throw new InvalidOperationException("This car is already rented on the chosen dates.");
            }

            var rentContract = _mapper.Map<RentContract>(contractDTO);
            // TODO заменить на реальный UserID
            rentContract.UserId = 1;
            rentContract.CreatedAt = DateTime.Now;
            rentContract.ContractStatusId = (int)RentContractStatus.PendingConfirmation;

            return await _rentContractRepository.SaveRentContract(rentContract);
        }

        public async Task UpdateRentContract(RentContractUpateDTO contractDTO)
        {
            //TODO заменить на реальный UserId и кастомную ошибку
            var contract = await _rentContractRepository.GetRentContract(contractDTO.RentContractId);

            if (contract.UserId != 1)
            {
                throw new UnauthorizedAccessException("Not enough rights to update this rent.");
            }

            var rentContract = _mapper.Map<RentContract>(contractDTO);
            await _rentContractRepository.UpdateRentContract(rentContract);
        }
    }
}
