using ContractService.Config;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Repositories.Impl
{
    public class DepositDisputeRepositoryImpl : IDepositDisputeRepository
    {
        private readonly ApplicationContext _context;
        public DepositDisputeRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<DepositDispute> GetDepositDispute(int depositDisputeId)
        {

            var depositDispute = await _context.DepositeDisputes
                                .FirstOrDefaultAsync(d => d.DepositDisputeId == depositDisputeId);
            if (depositDispute == null)
            {
                throw new KeyNotFoundException($"Deposit dispute with ID {depositDisputeId} not found.");
            }
            return depositDispute;

        }

        public async Task<DepositDispute> GetDepositDisputeByRenalId(int rentalId)
        {
           var dispute = await _context.Contracts
                .Include(c => c.DepositDispute)
                .Where(c => c.RentalId == rentalId)
                .Select(c => c.DepositDispute)
                .FirstOrDefaultAsync();
            if (dispute == null)
            {
                throw new KeyNotFoundException($"Deposit dispute  not found.");
            }
            return dispute;
        }

        public async Task UpdateDispute(DepositDispute disputeUpdate)
        {

            _context.Attach(disputeUpdate);
            _context.Entry(disputeUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}