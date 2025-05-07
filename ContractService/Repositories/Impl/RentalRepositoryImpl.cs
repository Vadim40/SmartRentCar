using ContractService.Config;
using ContractService.DTOs;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;
using SmartRentCar.Models;

namespace ContractService.Repositories.Impl
{
    public class RentalRepositoryImpl : IRentalRepository
    {
        private readonly ApplicationContext _context;
        public RentalRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<string> GetContractAddress(int rentalId)
        {
            var address = await _context.Contracts
                            .Where(c => c.RentalId == rentalId)
                            .Select( c => c.ContractAddress)
                            .FirstOrDefaultAsync();
            if (address == null)
            {
                throw new KeyNotFoundException($"Address not found");
            }
            return address;
        }

        public async Task<Rental> GetRental(int rentalId)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == rentalId);
            if (rental == null)
            {
                throw new KeyNotFoundException($"Rental with ID {rentalId} not found.");
            }
            return rental;
        }

        public async Task<List<RentalDocument>> GetRentalDocuments(int rentalId)
        {
            return await _context.RentalDocuments
                .Where(rd => rd.RentalId == rentalId)
                .ToListAsync();
        }

        public async Task<List<Rental>> GetRentals(FilterToRents filter)
        {
            return await _context.Rentals
                .Where(r =>
                    (filter.RentalStatuses == null || filter.RentalStatuses.Contains(r.RentalStatusId)) &&
                    (filter.StartDate == null || filter.EndDate == null || (filter.StartDate <= r.EndDate && filter.EndDate >= r.StartDate)) &&
                    _context.Cars.Any(car => car.CarName.Contains(filter.CarName))
                )
                .ToListAsync();
        }

        public async Task<List<Models.RentalStatus>> GetRentalStatuses()
        {
            return await _context.RentalStatuses.ToListAsync();
        }

        public async Task UpdateRentalStatus(int rentalId, int rentalStatusId)
        {
            var rental = await GetRental(rentalId);
            rental.RentalStatusId = rentalStatusId;
            await _context.SaveChangesAsync();
        }

    }
}