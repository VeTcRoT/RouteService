using AutoMapper;
using RouteService.Application.Features.Routes.Commands.BookRide;
using RouteService.Application.Features.Routes.Queries.GetAvailableRoutes;
using RouteService.Domain.Entities;

namespace RouteService.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ride, RideDto>()
                .ForMember(dest => dest.ExtraInfo, opt => opt.MapFrom(src => src.RouteInfo.ExtraInfo));

            CreateMap<Ride, BookRideDto>()
                .ForMember(dest => dest.ExtraInfo, opt => opt.MapFrom(src => src.RouteInfo.ExtraInfo));
        }
    }
}
