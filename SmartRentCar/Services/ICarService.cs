using SmartRentCar.DTO;

namespace SmartRentCar.Services
{
    public interface ICarService
    {
        public Task<List<CarDTO>> GetCarsByFilter(FilterToCars filter);
        public Task<CarDTO> GetCarById(int carId);
        public Task<List<CarBookingDTO>> GetCarBookings( int carId);
        public Task<int> SaveCar(CarDTO car);
        public Task UpdateCar(CarDTO car);

        public Task DeleteCarById(int carId);

        public Task<List<CarImageDTO>> GetCarImagesById( int carId);
        public Task<CarImageDTO> GetCarImageById(int carId);

        public Task<List<CarBrandDTO>> GetCarBrands();
        public Task<List<CarClassDTO>> GetCarClasses();
        public Task<List<CarFuelTypeDTO>> GetCarFuelTypes();
        public Task<List<CarTransmissionDTO>> GetCarTransmissions();
    }
}
