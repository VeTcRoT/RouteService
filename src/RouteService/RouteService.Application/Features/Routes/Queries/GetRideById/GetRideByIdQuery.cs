using MediatR;
using RouteService.Application.Features.Routes.Queries.GetAvailableRoutes;

namespace RouteService.Application.Features.Routes.Queries.GetRideById
{
    public class GetRideByIdQuery : IRequest<RideDto>
    {
        public int RideId { get; set; }
    }
}
