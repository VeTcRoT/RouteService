using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteService.Application.Features.RoutesInfo.Queries.ListAllRouteInfo;

namespace RouteService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("listallrouteinfo", Name = "ListAllRouteInfo")]
        public async Task<IActionResult> ListAllRouteInfo()
        {
            var routeInfos = await _mediator.Send(new ListAllRouteInfoQuery());

            return Ok(routeInfos);
        }
    }
}
