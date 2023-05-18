using RouteService.Domain.Entities;

namespace RouteService.Application.Features.Routes.Commands.BookRide
{
    public class BookRideDto
    {
        public bool IsSuccess { get; set; }
        public ICollection<string>? Errors { get; set; }
        public string RouteId { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public ICollection<SeatDto> Seats { get; set; } = null!;
        public RouteDto? RouteInfo { get; set; }
    }
}
