using MediatR;

namespace RouteService.Application.Features.RoutesInfo.Commands.UpdateRouteInfo
{
    public class UpdateRouteInfoCommand : IRequest
    {
        public int Id { get; set; }
        public string ExtraInfo { get; set; } = string.Empty;
    }
}
