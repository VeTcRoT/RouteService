using MediatR;

namespace RouteService.Application.Features.RoutesInfo.Commands.DeleteRouteInfo
{
    public class DeleteRouteInfoCommand : IRequest
    {
        public int RouteInfoId { get; set; }
    }
}
