using ContractService.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<DisputeMessage> DisputeMessages => Set<DisputeMessage>();

    }
}
