using AutoMapper;
using RouteService.Application.Features.Routes.Commands.BookRide;
using RouteService.Domain.Entities;

namespace RouteService.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ride, BookRideDto>();
            CreateMap<Route, RouteDto>();
        }
    }
}
