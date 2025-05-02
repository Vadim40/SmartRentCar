using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Config
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Contract> BlockchainContracts => Set<Contract>();
        public DbSet<RentalStatus> ContractStatuses => Set<RentalStatus>();
        public DbSet<DepositDispute> DepositProcessings => Set<DepositDispute>();
        public DbSet<DisputeStatus> ProcessingStatuses => Set<DisputeStatus>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=contracts.db");
        }
    }
}
