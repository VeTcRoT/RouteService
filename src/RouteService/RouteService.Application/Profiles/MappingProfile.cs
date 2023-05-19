using AutoMapper;
using RouteService.Application.Features.Routes.Commands.BookRide;
using RouteService.Application.Features.Routes.Commands.CreateRide;
using RouteService.Application.Features.Routes.Commands.UpdateRide;
using RouteService.Application.Features.Routes.Queries.GetAvailableRoutes;
using RouteService.Application.Features.Routes.Queries.ListAllRides;
using RouteService.Application.Features.RoutesInfo.Commands.CreateRouteInfo;
using RouteService.Application.Features.RoutesInfo.Commands.UpdateRouteInfo;
using RouteService.Application.Features.RoutesInfo.Queries.GetRouteInfoById;
using RouteService.Domain.Dtos;
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

            CreateMap<Ride, ListAllRidesDto>()
                .ForMember(dest => dest.ExtraInfo, opt => opt.MapFrom(src => src.RouteInfo.ExtraInfo));

            CreateMap<CreateRideCommand, Ride>();

            CreateMap<Ride, CreateRideDto>();

            CreateMap<UpdateRideCommand, Ride>();

            CreateMap<Route, RouteInfoDto>();

            CreateMap<CreateRouteInfoCommand, Route>();

            CreateMap<UpdateRouteInfoCommand, Route>();
        }
    }
}
