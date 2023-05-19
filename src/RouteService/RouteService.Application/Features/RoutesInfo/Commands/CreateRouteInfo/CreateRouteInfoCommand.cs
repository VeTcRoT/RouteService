using MediatR;
using RouteService.Domain.Dtos;

namespace RouteService.Application.Features.RoutesInfo.Commands.CreateRouteInfo
{
    public class CreateRouteInfoCommand : IRequest<RouteInfoDto>
    {
        public string ExtraInfo { get; set; } = string.Empty;
    }
}
