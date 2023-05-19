using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteService.Application.Features.RoutesInfo.Commands.CreateRouteInfo;
using RouteService.Application.Features.RoutesInfo.Commands.DeleteRouteInfo;
using RouteService.Application.Features.RoutesInfo.Commands.UpdateRouteInfo;
using RouteService.Application.Features.RoutesInfo.Queries.GetRouteInfoById;
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

        [HttpGet("{routeInfoId}", Name = "GetRouteInfoById")]
        public async Task<IActionResult> GetRouteInfoById(int routeInfoId)
        {
            var routeInfo = await _mediator.Send(new GetRouteInfoByIdQuery() { RouteInfoId = routeInfoId });

            return Ok(routeInfo);
        }

        [HttpPost(Name = "CreateRouteInfo")]
        public async Task<IActionResult> CreateRouteInfo([FromBody] CreateRouteInfoCommand request)
        {
            var routeInfo = await _mediator.Send(request);

            return Ok(routeInfo);
        }

        [HttpPut(Name = "UpdateRouteInfo")]
        public async Task<IActionResult> UpdateRouteInfo([FromBody] UpdateRouteInfoCommand request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpDelete("{routeInfoId}", Name = "DeleteRouteInfo")]
        public async Task<IActionResult> DeleteRouteInfo(int routeInfoId)
        {
            await _mediator.Send(new DeleteRouteInfoCommand() { RouteInfoId = routeInfoId });

            return Ok();
        }
    }
}
