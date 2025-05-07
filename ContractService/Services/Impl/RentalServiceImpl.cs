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
            int statusId = (int) RentalStatus.Completed;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
        }

        public async Task<RentalDTO> GetRental(int rentalId)
        {
            var rental = await _rentalRepository.GetRental(rentalId);
            var rentalDTO = _mapper.Map<RentalDTO>(rental);
            rentalDTO.ContractAddress = await _rentalRepository.GetContractAddress(rentalId);
            return rentalDTO;
        }

        public async Task<List<RentalDTO>> GetRentals(FilterToRents filter)
        {
            var rentals = await _rentalRepository.GetRentals(filter);
            var rentalDTOs = _mapper.Map<List<RentalDTO>>(rentals);
            foreach (RentalDTO rental in rentalDTOs)
            {
                rental.ContractAddress = await _rentalRepository.GetContractAddress(rental.RentalId);
            }
            return rentalDTOs;
        }

        public async Task<List<RentalStatusDTO>> GetRentalStatuses()
        {
            var statuses = await _rentalRepository.GetRentalStatuses();
            return _mapper.Map<List<RentalStatusDTO>>(statuses);
        }

        public async Task InitiateDispute(int rentalId)
        {
             int statusId = (int) RentalStatus.PendingResolution;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
        }
    }
}