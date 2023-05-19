using MediatR;
using RouteService.Domain.Entities;

namespace RouteService.Application.Features.Routes.Queries.GetAvailableRoutes
{
    public class GetAvailableRoutesQuery : IRequest<IEnumerable<RideDto>?>
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
