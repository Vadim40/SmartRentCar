using ContractService.DTOs;
using ContractService.Models;


namespace ContractService.Repositories
{
    public interface IRentalRepository
    {
        public Task<Rental> GetRental(int rentalId);
        public Task<string> GetContractAddress(int rentalId);
        public Task<List<Rental>> GetRentalsToArbiter(FilterToRents filter);
        public Task<List<Rental>> GetRentalsByCompany(FilterToRents filter, int companyId);
        public Task UpdateRentalStatus(int rentalId, int rentalStatusId);
        public Task<List<Models.RentalStatus>> GetRentalStatuses();
        public Task<List<RentalDocument>> GetRentalDocuments(int rentalId);
        Task<(bool Company, bool Renter)> GetStartConfirmations(int rentalId);
        Task<(bool Company, bool Renter)> GetEarlyEndConfirmations(int rentalId);
        Task<(bool Company, bool Renter)> GetEndConfirmations(int rentalId);
        Task<bool> GetDisputeRequest(int rentalId);

        Task ConfirmStartByCompany(int rentalId);
        Task ConfirmStartByRenter(int rentalId);

        Task ConfirmEarlyEndByCompany(int rentalId);
        Task ConfirmEarlyEndByRenter(int rentalId);

        Task ConfirmEndByCompany(int rentalId);
        Task ConfirmEndByRenter(int rentalId);
        Task BeginDispute(int rentalId);
    }
}