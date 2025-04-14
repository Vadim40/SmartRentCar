using Microsoft.EntityFrameworkCore;
using SmartRentCar.Config;
using SmartRentCar.DTO;
using SmartRentCar.Models;

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
            var contract = await _context.RentContracts.FindAsync(contractId);
            if (contract != null)
            {
                _context.RentContracts.Remove(contract);
                await  _context.SaveChangesAsync();
            }
        }

        public Task<List<RentContract>> GetRentContractsByStatus(int userId, int statusId)
        {
            return _context.RentContracts
                .Include(r => r.ContractStatus)
                .Where(contract =>
                    contract.UserId == userId &&
                    contract.ContractStatusId == statusId)
                .ToListAsync();
        }


        public async Task<int> SaveRentContract(RentContract contract)
        {
           _context.Add(contract);
           await _context.SaveChangesAsync();
           return contract.RentContractId;
        }

        public async Task UpdateRentContract(RentContract contract)
        {
            _context.Update(contract);
            await _context.SaveChangesAsync();
        }
    }
}
