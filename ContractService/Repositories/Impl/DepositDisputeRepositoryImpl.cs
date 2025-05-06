using ContractService.Config;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Repositories.Impl
{
    public class DepositDisputeRepositoryImpl : IDepositDisputeRepository
    {
        private readonly ApplicationContext _context;
        public DepositDisputeRepositoryImpl(ApplicationContext context){
            _context = context;
        }
        public async Task<DepositDispute> GetDepositDispute(int depositDisputeId)
        {

                var depositDispute = await _context.DepositeDisputes
                                    .FirstOrDefaultAsync(d => d.DepositDisputeId == depositDisputeId);
                if(depositDispute == null){
                    throw new KeyNotFoundException($"Deposit dispute with ID {depositDisputeId} not found.");
                }
                return depositDispute;
            
        }

        public async Task UpdateDisputeStatus(int depositDisputeId, int disputeStatusId)
        {

                var depositDispute = await GetDepositDispute(depositDisputeId);
                depositDispute.DisputeStatusId = disputeStatusId;
                await _context.SaveChangesAsync();
        }
    }
}