using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Config
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<BlockchainContract> BlockchainContracts => Set<BlockchainContract>();
        public DbSet<ContractStatus> ContractStatuses => Set<ContractStatus>();
        public DbSet<DepositProcessing> DepositProcessings => Set<DepositProcessing>();
        public DbSet<ProcessingStatus> ProcessingStatuses => Set<ProcessingStatus>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=contracts.db");
        }
    }
}
