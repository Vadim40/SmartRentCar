using AutoMapper;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Repositories;

namespace SmartRentCar.Services.Impl
{
    public class CarServiceImpl : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public CarServiceImpl(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task DeleteCarById(int carId)
        {
            try
            {
                await _carRepository.DeleteCarById(carId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CarBrandDTO>> GetCarBrands()
        {
            try
            {
                var carBrands = await _carRepository.GetCarBrands();
                return _mapper.Map<List<CarBrandDTO>>(carBrands);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CarDTO> GetCarById(int carId)
        {
            try
            {
                var car = await _carRepository.GetCarById(carId);
                return _mapper.Map<CarDTO>(car);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CarClassDTO>> GetCarClasses()
        {
            try
            {
                var carClasses = await _carRepository.GetCarClasses();
                return _mapper.Map<List<CarClassDTO>>(carClasses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CarFuelTypeDTO>> GetCarFuelTypes()
        {
            try
            {
                var carFuelTypes = await _carRepository.GetCarFuelTypes();
                return _mapper.Map<List<CarFuelTypeDTO>>(carFuelTypes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CarImageDTO> GetCarImageById(int carId)
        {
            try
            {
                var carImage = await _carRepository.GetFirstCarImageById(carId);
                return _mapper.Map<CarImageDTO>(carImage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<CarImageDTO>> GetCarImagesById(int carId)
        {
            try
            {
                var carImages = await _carRepository.GetCarImagesById(carId);
                return _mapper.Map<List<CarImageDTO>>(carImages);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CarDTO>> GetCarsByFilter(FilterToCars filter)
        {
            try
            {
                var cars = await _carRepository.GetCarsByFilter(filter);
                return _mapper.Map<List<CarDTO>>(cars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CarTransmissionDTO>> GetCarTransmissions()
        {
            try
            {
                var carTransmissions = await _carRepository.GetCarTransmissions();
                return _mapper.Map<List<CarTransmissionDTO>>(carTransmissions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> SaveCar(CarDTO carDTO)
        {
            try
            {
                var car = _mapper.Map<Car>(carDTO);
                return _carRepository.SaveCar(car);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCar(CarDTO carDTO)
        {
            try
            {
                var car = _mapper.Map<Car>(carDTO);
                await _carRepository.UpdateCar(car);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
