using AutoMapper;
using ContractService.DTOs;
using ContractService.Models;

namespace ContractService.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Rental, RentalDTO>()
            .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => src.Car.CarName))
    .       ForMember(dest => dest.RentalStatusName, opt => opt.MapFrom(src => src.RentalStatus.Name));


             CreateMap<DepositDispute, DepositDisputeDTO>()
            .ForMember(dest => dest.DisputeStatusName, opt => opt.MapFrom(src => src.DisputeStatus.Name));

            CreateMap<DisputeUpdateDTO, DepositDispute>();
            CreateMap<Models.RentalStatus, RentalStatusDTO>();

            CreateMap<DisputeMessage, DisputeMessageDTO>();
             CreateMap<DisputeMessageDTO, DisputeMessage>();
        }
    }
}