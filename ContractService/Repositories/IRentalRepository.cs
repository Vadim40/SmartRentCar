using ContractService.DTOs;
using SmartRentCar.Models;

namespace ContractService.Repositories
{
    public interface IRentalRepository
    {
        public Task<Rental> GetRental(int rentalId);
        public Task<List<Rental>> GetRentals(FilterToRents filter);
        public Task UpdateRentalStatus(int rentalId, int rentalStatus);
    }
}