using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteService.Application.Features.Routes.Commands.BookRide;
using RouteService.Application.Features.Routes.Commands.CreateRide;
using RouteService.Application.Features.Routes.Queries.GetAvailableRoutes;
using RouteService.Application.Features.Routes.Queries.GetRideById;
using RouteService.Application.Features.Routes.Queries.ListAllRides;

namespace RouteService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("getavailableroutes", Name = "GetAvailableRoutes")]
        public async Task<IActionResult> GetAvailableRoutes([FromBody] GetAvailableRoutesQuery request)
        {
            var routes = await _mediator.Send(request);

            return Ok(routes);
        }
        [HttpPost("bookride", Name = "BookRide")]
        public async Task<IActionResult> BookRide([FromBody] BookRideCommand request)
        {
            var route = await _mediator.Send(request);

            return Ok(route);
        }

        [HttpGet("{rideId}", Name = "GetRideById")]
        public async Task<IActionResult> GetRideById(int rideId)
        {
            var ride = await _mediator.Send(new GetRideByIdQuery() { RideId = rideId });

            return Ok(ride);
        }

        [HttpGet("listallrides", Name = "ListAllRides")]
        public async Task<IActionResult> ListAllRides()
        {
            var rides = await _mediator.Send(new ListAllRidesQuery());

            return Ok(rides);
        }

        [HttpPost("createride", Name = "CreateRide")]
        public async Task<IActionResult> CreateRide([FromBody] CreateRideCommand request)
        {
            var createdRide = await _mediator.Send(request);

            return Ok(createdRide);
        }
    }
}
