using Microsoft.EntityFrameworkCore;
using SmartRentCar.Config;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Models.CarInfo;

namespace SmartRentCar.Repositories.Impl
{
    public class CarRepositoryImpl : ICarRepository
    {
        private readonly ApplicationContext _context;
        public CarRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }
        public async Task DeleteCar(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);

            if (car != null)
            {
                _context.Cars.Remove(car);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Car> GetCar(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car == null)
            {
                throw new KeyNotFoundException($"Car with Id {carId} not found");
            }
            return car;
        }

        public async Task<List<CarBrand>> GetCarBrands()
        {
            return await _context.CarBrands.ToListAsync();
        }


        public async Task<List<CarClass>> GetCarClasses()
        {
            return await _context.CarClasses.ToListAsync();
        }

        public async Task<List<CarFuelType>> GetCarFuelTypes()
        {
            return await _context.CarFuelTypes.ToListAsync();
        }

        public Task<List<CarImage>> GetCarImages(int carId)
        {
            var query =  _context.CarImages.Where(carImage =>
            carImage.CarId.Equals(carId));
            return query.ToListAsync();
        }

        public async Task<List<Car>> GetCars(FilterToCars filter)
        {
            var query = _context.Cars.Where(car =>
               (!filter.CostMin.HasValue || car.CostPerDay >= filter.CostMin.Value) &&
               (!filter.CostMax.HasValue || car.CostPerDay <= filter.CostMax.Value) &&
               (!filter.DepositMin.HasValue || car.DepositAmount >= filter.DepositMin.Value) &&
               (!filter.DepositMax.HasValue || car.DepositAmount <= filter.DepositMax.Value) &&
               (filter.CarBrands == null || filter.CarBrands.Count == 0 || filter.CarBrands.Contains(car.BrandId)) &&
               (filter.CarClasses == null || filter.CarClasses.Count == 0 || filter.CarClasses.Contains(car.ClassId)) &&
               (!filter.CarTransmission.HasValue || car.TransmissionTypeId == filter.CarTransmission.Value) &&
               (!filter.CarFuel.HasValue || car.FuelTypeId == filter.CarFuel.Value)
               );

            return await query.ToListAsync();
        }

        public async Task<List<CarTransmission>> GetCarTransmissions()
        {
            return await _context.CarTransmissions.ToListAsync();
        }

        public async Task<CarImage> GetFirstCarImage(int carId)
        {
            var carImage = await _context.CarImages
                .FirstOrDefaultAsync(carImage => carImage.CarId == carId);

            return carImage;
        }


        public async Task<int> SaveCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car.CarId;
        }


        public async Task UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
    }
}
