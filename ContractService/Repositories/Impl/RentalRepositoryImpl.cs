using ContractService.Config;
using ContractService.DTOs;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;


namespace ContractService.Repositories.Impl
{
    public class RentalRepositoryImpl : IRentalRepository
    {
        private readonly ApplicationContext _context;
        public RentalRepositoryImpl(ApplicationContext context)
        {
            _context = context;
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

        public async Task<List<Rental>> GetRentalsToArbiter(FilterToRents filter)
        {
            return await _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.RentalStatus)
                .Where(r =>
                    r.RentalStatusId == (int)DTOs.RentalStatus.PendingArbitration &&
                    (filter.StartDate == null || filter.EndDate == null || (filter.StartDate <= r.EndDate && filter.EndDate >= r.StartDate)) &&
                    (filter.CarName == null || r.Car.CarName.Contains(filter.CarName))
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

        public async Task<List<Rental>> GetRentalsByCompany(FilterToRents filter, int companyId)
        {
            return await _context.Rentals
               .Include(r => r.Car)
               .Include(r => r.RentalStatus)
               .Where(r =>
                   r.CompanyId == companyId &&
                   (filter.RentalStatuses == null || filter.RentalStatuses.Contains(r.RentalStatusId)) &&
                   (filter.StartDate == null || filter.EndDate == null || (filter.StartDate <= r.EndDate && filter.EndDate >= r.StartDate)) &&
                   (filter.CarName == null || r.Car.CarName.Contains(filter.CarName))
               )
               .ToListAsync();
        }

        public async Task<(bool Company, bool Renter)> GetStartConfirmations(int rentalId)
        {
            var rental = await _context.Rentals
                .Where(r => r.RentalId == rentalId)
                .Select(r => new
                {
                    r.IsStartConfirmedByCompany,
                    r.IsStartConfirmedByRenter
                })
                .FirstOrDefaultAsync();

            return rental == null
                ? (false, false)
                : (rental.IsStartConfirmedByCompany, rental.IsStartConfirmedByRenter);
        }

        public async Task<(bool Company, bool Renter)> GetEarlyEndConfirmations(int rentalId)
        {
            var rental = await _context.Rentals
                .Where(r => r.RentalId == rentalId)
                .Select(r => new
                {
                    r.IsEarlyEndConfirmedByCompany,
                    r.IsEarlyEndConfirmedByRenter
                })
                .FirstOrDefaultAsync();

            return rental == null
                ? (false, false)
                : (rental.IsEarlyEndConfirmedByCompany, rental.IsEarlyEndConfirmedByRenter);
        }

        public async Task<(bool Company, bool Renter)> GetEndConfirmations(int rentalId)
        {
            var rental = await _context.Rentals
                .Where(r => r.RentalId == rentalId)
                .Select(r => new
                {
                    r.IsEndConfirmedByCompany,
                    r.IsEndConfirmedByRenter
                })
                .FirstOrDefaultAsync();

            return rental == null
                ? (false, false)
                : (rental.IsEndConfirmedByCompany, rental.IsEndConfirmedByRenter);
        }
        public async Task ConfirmStartByCompany(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.IsStartConfirmedByCompany = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ConfirmStartByRenter(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.IsStartConfirmedByRenter = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ConfirmEarlyEndByCompany(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.IsEarlyEndConfirmedByCompany = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ConfirmEarlyEndByRenter(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.IsEarlyEndConfirmedByRenter = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ConfirmEndByCompany(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.IsEndConfirmedByCompany = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ConfirmEndByRenter(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.IsEndConfirmedByRenter = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> GetDisputeRequest(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            return rental?.IsDisputeRequested ?? false;

        }

        public async Task BeginDispute(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.IsDisputeRequested = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetContractAddress(int rentalId)
        {
            var contractAddress = await _context.Rentals
                .Where(r => r.RentalId == rentalId)
                .Select(r => r.ContractAddress)
                .FirstOrDefaultAsync();

            if (contractAddress == null)
            {
                throw new KeyNotFoundException($"Rental with ID {rentalId} not found.");
            }

            return contractAddress;
        }

    }
}