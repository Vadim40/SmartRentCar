using AutoMapper;
using ContractService.DTOs;
using ContractService.Models;
using SmartRentCar.Models;

namespace ContractService.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rental, RentalDTO>()
            .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => src.Car.CarName));

             CreateMap<DepositDispute, DepositDisputeDTO>()
            .ForMember(dest => dest.DisputeStatusName, opt => opt.MapFrom(src => src.DisputeStatus.Name));

            CreateMap<RentalStatus, RentalStatusDTO>();
        }
    }
}