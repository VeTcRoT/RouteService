using MediatR;

namespace RouteService.Application.Features.Routes.Queries.ListAllRides
{
    public class ListAllRidesQuery : IRequest<IEnumerable<ListAllRidesDto>>
    {
    }
}
