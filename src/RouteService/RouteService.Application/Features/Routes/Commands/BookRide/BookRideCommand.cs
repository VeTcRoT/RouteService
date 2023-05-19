using MediatR;

namespace RouteService.Application.Features.Routes.Commands.BookRide
{
    public class BookRideCommand : IRequest<BookRideDto>
    {
        public string RouteId { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
    }
}
