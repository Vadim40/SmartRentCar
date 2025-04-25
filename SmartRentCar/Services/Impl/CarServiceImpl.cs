using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartRentCar.Config;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Repositories;

namespace SmartRentCar.Services.Impl
{
    public class CarServiceImpl : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CarServiceImpl(ICarRepository carRepository, IMapper mapper, ApplicationContext context)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task DeleteCarById(int carId)
        {
            await _carRepository.DeleteCarById(carId);
        }

        public async Task<List<CarBookingDTO>> GetCarBookings(int carId)
        {
            // TODO поменять
            return await _context.RentContracts
                .Where(r => r.ContractStatusId != (int)RentContractStatus.Canceled && r.CarId == carId && r.EndDate > DateTime.Now)
                .Select(r => new CarBookingDTO
                {
                    StartDate = r.StartDate,
                    EndDate = r.EndDate
                })
                .ToListAsync();
        }

        public async Task<List<CarBrandDTO>> GetCarBrands()
        {
            var carBrands = await _carRepository.GetCarBrands();
            return _mapper.Map<List<CarBrandDTO>>(carBrands);
        }

        public async Task<CarDTO> GetCarById(int carId)
        {
            var car = await _carRepository.GetCarById(carId);
            return _mapper.Map<CarDTO>(car);
        }

        public async Task<List<CarClassDTO>> GetCarClasses()
        {
            var carClasses = await _carRepository.GetCarClasses();
            return _mapper.Map<List<CarClassDTO>>(carClasses);
        }

        public async Task<List<CarFuelTypeDTO>> GetCarFuelTypes()
        {
            var carFuelTypes = await _carRepository.GetCarFuelTypes();
            return _mapper.Map<List<CarFuelTypeDTO>>(carFuelTypes);
        }

        public async Task<CarImageDTO> GetCarImageById(int carId)
        {
            var carImage = await _carRepository.GetFirstCarImageById(carId);
            return _mapper.Map<CarImageDTO>(carImage);
        }

        public async Task<List<CarImageDTO>> GetCarImagesById(int carId)
        {
            var carImages = await _carRepository.GetCarImagesById(carId);
            return _mapper.Map<List<CarImageDTO>>(carImages);
        }

        public async Task<List<CarDTO>> GetCarsByFilter(FilterToCars filter)
        {
            var cars = await _carRepository.GetCarsByFilter(filter);
            return _mapper.Map<List<CarDTO>>(cars);
        }

        public async Task<List<CarTransmissionDTO>> GetCarTransmissions()
        {
            var carTransmissions = await _carRepository.GetCarTransmissions();
            return _mapper.Map<List<CarTransmissionDTO>>(carTransmissions);
        }

        public async Task<int> SaveCar(CarDTO carDTO)
        {
            var car = _mapper.Map<Car>(carDTO);
            return await _carRepository.SaveCar(car);
        }

        public async Task UpdateCar(CarDTO carDTO)
        {
            var car = _mapper.Map<Car>(carDTO);
            await _carRepository.UpdateCar(car);
        }
    }
}
