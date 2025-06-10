using ContractService.DTOs;

namespace ContractService.Services
{
    public interface IRentalService
    {
        public  Task<List<RentalDTO>> GetRentals(FilterToRents filter, string role, int userId);
        public Task<RentalDTO> GetRental(int rentalId);
        public Task ApproveRental( int rentalId);

        public Task InitiateDispute(int renalId);

        public Task<List<RentalStatusDTO>> GetRentalStatuses();
        Task ConfirmEarlyEnd(int rentalId);
        Task ConfirmEnd(int rentalId);
        Task SendToArbitration(int rentalId);
        Task ConfirmStart(int rentalId);
    }
}