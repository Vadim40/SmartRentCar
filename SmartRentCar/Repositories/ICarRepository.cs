using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Models.CarInfo;

namespace SmartRentCar.Repositories
{
    public interface ICarRepository
    {
        public Task<List<Car>> GetCarsByFilter(FilterToCars filter);
        public Task<Car> GetCarById(int carId);
        
        public Task<int> SaveCar (Car car);
        public Task UpdateCar (Car car);

        public Task DeleteCarById (int carId);

        public Task<List<CarBrand>> GetCarBrands();
        public Task<List<CarClass>> GetCarClasses();
        public Task<List<CarFuelType>> GetCarFuelTypes();
        public Task<List<CarTransmission>> GetCarTransmissions();

        public Task<List<CarImage>> GetCarImages(int carId);
        public Task<CarImage> GetFirstCarImage(int carId);

    }
}
