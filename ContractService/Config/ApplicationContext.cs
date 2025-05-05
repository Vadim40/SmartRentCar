using ContractService.Models;
using Microsoft.EntityFrameworkCore;
using SmartRentCar.Models;

namespace ContractService.Config
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<RentalStatus> RentalStatuses => Set<RentalStatus>();
        public DbSet<DepositDispute> DepositeDisputes => Set<DepositDispute>();
        public DbSet<DisputeStatus> DisputeStatuses => Set<DisputeStatus>();
        public DbSet<Rental> Rentals => Set<Rental>();
        public DbSet<RentalDocument> RentalDocuments => Set<RentalDocument>();
        public DbSet<Car> Cars => Set<Car>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=contracts.db");
        }
    }
}
