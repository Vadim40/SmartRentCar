using SmartRentCar.Models;
using SmartRentCar.Models.CarInfo;

namespace SmartRentCar.Repositories
{
    public interface ICarRepository
    {
        public Task<List<Car>> GetCars();
        public Task<Car> GetCar(int carId);
        
        public Task SaveCar (Car car);
        public Task UpdateCar (Car car);

        public Task DeleteCar (int carId);

        public Task<CarBrand> GetCarBrands();
        public Task<CarClass> GetCarClasses();
        public Task<CarFuelType> GetCarFuelTypes();
        public Task<CarTransmission> GetCarTransmissions();

    }
}
