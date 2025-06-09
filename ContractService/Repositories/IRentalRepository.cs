using ContractService.DTOs;
using ContractService.Models;


namespace ContractService.Repositories
{
    public interface IRentalRepository
    {   
        public Task<Rental> GetRental(int rentalId);
        public Task<List<Rental>> GetRentalsToArbiter(FilterToRents filter);
        public Task<List<Rental>> GetRentalsByCompany(FilterToRents filter, int companyId);
        public Task UpdateRentalStatus(int rentalId, int rentalStatusId);
        public Task<List<Models.RentalStatus>> GetRentalStatuses(); 
        public Task<List<RentalDocument>> GetRentalDocuments(int rentalId);

    }
}