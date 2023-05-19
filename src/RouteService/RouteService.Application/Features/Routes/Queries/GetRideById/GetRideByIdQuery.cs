using MediatR;
using RouteService.Domain.Dtos;

namespace RouteService.Application.Features.Routes.Queries.GetRideById
{
    public class GetRideByIdQuery : IRequest<RideDto>
    {
        public int RideId { get; set; }
    }
}
