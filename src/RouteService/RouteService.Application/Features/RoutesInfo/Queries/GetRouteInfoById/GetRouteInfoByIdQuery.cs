using MediatR;
using RouteService.Domain.Dtos;

namespace RouteService.Application.Features.RoutesInfo.Queries.GetRouteInfoById
{
    public class GetRouteInfoByIdQuery : IRequest<RouteInfoDto>
    {
        public int RouteInfoId { get; set; }
    }
}
