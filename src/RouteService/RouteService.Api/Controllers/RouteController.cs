using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteService.Application.Features.Routes.Commands.BookRide;
using RouteService.Application.Features.Routes.Queries.GetAvailableRoutes;

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
    }
}
