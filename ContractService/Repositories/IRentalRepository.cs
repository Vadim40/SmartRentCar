using ContractService.DTOs;
using ContractService.Models;
using SmartRentCar.Models;

namespace ContractService.Repositories
{
    public interface IRentalRepository
    {   
        public Task<Rental> GetRental(int rentalId);
        public Task<List<Rental>> GetRentals(FilterToRents filter);
        public Task UpdateRentalStatus(int rentalId, int rentalStatusId);
        public Task<List<RentalStatus>> GetRentalStatuses(); 
        public Task<List<RentalDocument>> GetRentalDocuments(int rentId);
    }
}