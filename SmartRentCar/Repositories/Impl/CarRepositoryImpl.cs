using Microsoft.EntityFrameworkCore;
using SmartRentCar.Config;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Models.CarInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRentCar.Repositories.Impl
{
    public class CarRepositoryImpl : ICarRepository
    {
        private readonly ApplicationContext _context;
        public CarRepositoryImpl(ApplicationContext context)
        {
            _context = context;
        }

        public async Task DeleteCarById(int carId)
        {
            try
            {
                var car = await _context.Cars.FindAsync(carId);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Car> GetCarById(int carId)
        {
            try
            {
                var car = await _context.Cars
                    .Include(car => car.CarClass)
                    .Include(car => car.CarTransmission)
                    .Include(car => car.CarFuelType)
                    .Include(car => car.CarBrand)
                    .FirstOrDefaultAsync(car => car.CarId == carId);

                if (car == null)
                {
                    throw new KeyNotFoundException($"Car with ID {carId} not found.");
                }

                return car;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<CarBrand>> GetCarBrands()
        {
            try
            {
                return await _context.CarBrands.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<CarClass>> GetCarClasses()
        {
            try
            {
                return await _context.CarClasses.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<CarFuelType>> GetCarFuelTypes()
        {
            try
            {
                return await _context.CarFuelTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<CarImage>> GetCarImagesById(int carId)
        {
            try
            {
                return await _context.CarImages
                    .Where(carImage => carImage.CarId == carId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<Car>> GetCarsByFilter(FilterToCars filter)
        {
            try
            {
                var query = _context.Cars
                    .Include(car => car.CarClass)
                    .Include(car => car.CarTransmission)
                    .Include(car => car.CarFuelType)
                    .Include(car => car.CarBrand)
                    .Where(car =>
                        (!_context.RentContracts.Any(rent =>
                            rent.CarId == car.CarId &&
                            filter.StartDate <= rent.EndDate &&
                            filter.EndDate >= rent.StartDate)) &&
                        (!filter.CostMin.HasValue || car.CostPerDay >= filter.CostMin.Value) &&
                        (!filter.CostMax.HasValue || car.CostPerDay <= filter.CostMax.Value) &&
                        (!filter.DepositMin.HasValue || car.DepositAmount >= filter.DepositMin.Value) &&
                        (!filter.DepositMax.HasValue || car.DepositAmount <= filter.DepositMax.Value) &&
                        (filter.CarBrands == null || filter.CarBrands.Contains(car.BrandId)) &&
                        (filter.CarClasses == null || filter.CarClasses.Contains(car.ClassId)) &&
                        (!filter.CarTransmission.HasValue || car.TransmissionTypeId == filter.CarTransmission.Value) &&
                        (!filter.CarFuel.HasValue || car.FuelTypeId == filter.CarFuel.Value));

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<CarTransmission>> GetCarTransmissions()
        {
            try
            {
                return await _context.CarTransmissions.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<CarImage> GetFirstCarImageById(int carId)
        {
            try
            {
                return await _context.CarImages
                    .FirstAsync(c => c.CarId == carId);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> SaveCar(Car car)
        {
            try
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return car.CarId;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateCar(Car car)
        {
            try
            {
                _context.Cars.Update(car);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
