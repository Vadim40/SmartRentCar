using AutoMapper;
using ContractService.DTOs;
using ContractService.Models;
using ContractService.Repositories;

namespace ContractService.Services.Impl
{
    public class RentalServiceImpl : IRentalService
    {
        private readonly IMapper _mapper;
        private readonly IRentalRepository _rentalRepository;

        public RentalServiceImpl(IMapper mapper, IRentalRepository rentalRepository)
        {
            _mapper = mapper;
            _rentalRepository = rentalRepository;
        }

        public async Task ApproveRental(int rentalId)
        {
            int statusId = (int)DTOs.RentalStatus.Completed;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
        }

        public async Task<RentalDTO> GetRental(int rentalId)
        {
            var rental = await _rentalRepository.GetRental(rentalId);
            var rentalDTO = _mapper.Map<RentalDTO>(rental);
            return rentalDTO;
        }

        public async Task<List<RentalDTO>> GetRentals(FilterToRents filter, string role, int companyId)
        {
            List<Rental> rentals = new List<Rental> ();

            if (role == "intermediary")
            {
                rentals = await _rentalRepository.GetRentalsToArbiter(filter);
            }
            else if (role == "company_rep")
            {
                rentals = await _rentalRepository.GetRentalsByCompany(filter, companyId);
            }
            //TODO заглушка
             rentals = await _rentalRepository.GetRentalsByCompany(filter, 0);
            var rentalDTOs = _mapper.Map<List<RentalDTO>>(rentals);


            return rentalDTOs;
        }


        public async Task<List<RentalStatusDTO>> GetRentalStatuses()
        {
            var statuses = await _rentalRepository.GetRentalStatuses();
            return _mapper.Map<List<RentalStatusDTO>>(statuses);
        }

        public async Task InitiateDispute(int rentalId)
        {
            int statusId = (int)DTOs.RentalStatus.PendingArbitration;
            await _rentalRepository.UpdateRentalStatus(rentalId, statusId);
        }
    }
}