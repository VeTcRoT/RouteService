using MediatR;
using RouteService.Domain.Entities;

namespace RouteService.Application.Features.RoutesInfo.Queries.ListAllRouteInfo
{
    public class ListAllRouteInfoQuery : IRequest<IEnumerable<Route>>
    {
    }
}
