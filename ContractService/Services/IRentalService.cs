using ContractService.DTOs;

namespace ContractService.Services
{
    public interface IRentalService
    {
        public Task<List<RentalDTO>> GetRentals(FilterToRents filter);
        public Task<RentalDTO> GetRental(int rentalId);
        public Task ApproveRental( int rentalId);

        public Task InitiateDispute(int renalId);

        public Task<List<RentalStatusDTO>> GetRentalStatuses();

    }
}