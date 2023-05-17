using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> GetAvailableRoutes([FromBody]GetAvailableRoutesQuery request)
        {
            var routes = await _mediator.Send(request);

            return Ok(routes);
        }
    }
}
