using Microsoft.EntityFrameworkCore;
using SmartRentCar.Models;
using SmartRentCar.Models.CarInfo;

namespace SmartRentCar.Config
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Company> Companys => Set<Company>();
        public DbSet<RentContract> RentContracts => Set<RentContract>();
        public DbSet<ContractStatus> ContractStatuses => Set<ContractStatus>();
        public DbSet<CarBrand> carBrands => Set<CarBrand>();
        public DbSet<CarClass> CarClasses => Set<CarClass>();
        public DbSet<CarFuelType> carFuelTypes => Set<CarFuelType>();
        public DbSet<CarImage> carImages => Set<CarImage>();
        public DbSet<CarTransmission> carTransmissions => Set<CarTransmission>();

        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=smartRentCar.db");
        }
    }
}
