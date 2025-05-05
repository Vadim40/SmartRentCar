using AutoMapper;
using ContractService.DTOs;
using ContractService.Repositories;

namespace ContractService.Services.Impl
{
    public class RentalServiceImpl : IRentalService
    {
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;

        public RentalServiceImpl( IMapper mapper, IRentalRepository rentalRepository){
            _mapper = mapper;
            _rentalRepository = rentalRepository;
        }

        public async Task ApproveRental(int rentalId)
        {
            int statusId = (int) RentContractStatus.Completed;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
        }

        public async Task<RentalDTO> GetRental(int rentalId)
        {
            var rental = await _rentalRepository.GetRental(rentalId);
            return _mapper.Map<RentalDTO>(rental);
        }

        public async Task<List<RentalDTO>> GetRentals(FilterToRents filter)
        {
            var rentals = await _rentalRepository.GetRentals(filter);
            return _mapper.Map<List<RentalDTO>>(rentals);
        }

        public async Task<List<RentalStatusDTO>> GetRentalStatuses()
        {
            var statuses = await _rentalRepository.GetRentalStatuses();
            return _mapper.Map<List<RentalStatusDTO>>(statuses);
        }

        public async Task InitiateDispute(int rentalId)
        {
             int statusId = (int) RentContractStatus.PendingResolution;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
        }
    }
}