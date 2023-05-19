using MediatR;

namespace RouteService.Application.Features.Routes.Commands.DeleteRide
{
    public class DeleteRideCommand : IRequest
    {
        public int Id { get; set; }
    }
}
