using Microsoft.EntityFrameworkCore;
using SmartRentCar.Config;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartRentCar.Repositories.Impl
{
    public class RentContractRepositoryImpl : IRentContractRepository
    {
        private readonly ApplicationContext _context;
        public RentContractRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }

        public async Task DeleteRentContractById(int contractId)
        {
            try
            {
               var contract = await _context.RentContracts
                    .FirstOrDefaultAsync(c => c.RentContractId == contractId);

                if (contract != null)
                {
                    _context.RentContracts.Remove(contract);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<RentContract> GetRentContract(int contractId)
        {
            try
            {
                var contract = await _context.RentContracts
                    .FirstOrDefaultAsync(c => c.RentContractId == contractId);

                if (contract == null)
                {
                    throw new KeyNotFoundException($"RentContract with ID {contractId} not found.");
                }
                return contract;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<RentContract>> GetRentContracts(int userId)
        {
            try
            {
                return await _context.RentContracts
                    .Include(r => r.ContractStatus)
                    .Where(contract =>
                        contract.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<RentContract>> GetRentContractsByStatus(int userId, int statusId)
        {
            try
            {
                return await _context.RentContracts
                    .Include(r => r.ContractStatus)
                    .Where(contract =>
                        contract.UserId == userId &&
                        contract.ContractStatusId == statusId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> IsCarInRent(int carId, DateTime startDate, DateTime endDate)
        {
            return await _context.RentContracts
                .AnyAsync(r => r.CarId == carId && startDate <= r.EndDate && endDate >= r.StartDate);
        }


        public async Task<int> SaveRentContract(RentContract contract)
        {
            try
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return contract.RentContractId;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateRentContract(RentContract contract)
        {
            try
            {
                _context.Update(contract);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
