using AutoMapper;
using SmartRentCar.DTO;
using SmartRentCar.Models;
using SmartRentCar.Models.CarInfo;

namespace SmartRentCar.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<Company, CompanyDTO>();
            CreateMap<Car, CarDTO>()
            .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.CarClass.Name)) 
            .ForMember(dest => dest.FuelTypeName, opt => opt.MapFrom(src => src.CarFuelType.Name)) 
            .ForMember(dest => dest.TransmissionTypeName, opt => opt.MapFrom(src => src.CarTransmission.Name)); 


            CreateMap<CarImage, CarImageDTO>();

            CreateMap<CarClass, CarClassDTO>();
            CreateMap<CarBrand, CarBrandDTO>();
            CreateMap<CarFuelType, CarFuelTypeDTO>();
            CreateMap<CarTransmission, CarTransmissionDTO>();
        }
    }
}
