using MediatR;

namespace RouteService.Application.Features.RoutesInfo.Queries.GetRouteInfoById
{
    public class GetRouteInfoByIdQuery : IRequest<RouteInfoDto>
    {
        public int RouteInfoId { get; set; }
    }
}
