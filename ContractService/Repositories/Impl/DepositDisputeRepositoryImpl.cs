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

        public async Task CreateDisputeMessage(DisputeMessage disputeMessage)
        {
              _context.DisputeMessages.Add(disputeMessage);
              await _context.SaveChangesAsync();

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
           var contract  = await _context.Contracts
                .Include(c => c.DepositDispute)
                    .ThenInclude(d => d.DisputeStatus)
                .Where(c => c.RentalId == rentalId)
                .FirstOrDefaultAsync();
            if (contract.DepositDispute == null)
            {
                throw new KeyNotFoundException($"Deposit dispute  not found.");
            }
            return contract.DepositDispute;
        }

        public async Task<DisputeMessage> GetDisputeMessage(int depositDisputeId)
        {
           return await _context.DisputeMessages
                                .Where( d => d.DepositDisputeId == depositDisputeId)
                                .FirstOrDefaultAsync();
        }

        public async Task UpdateDispute(DepositDispute disputeUpdate)
        {

            _context.DepositeDisputes.Attach(disputeUpdate);
            _context.DepositeDisputes.Entry(disputeUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDisputeMessage(DisputeMessage disputeMessage)
        {
            _context.DisputeMessages.Attach(disputeMessage);
            _context.DisputeMessages.Entry(disputeMessage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}