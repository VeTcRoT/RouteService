using Microsoft.AspNetCore.Mvc;

namespace RouteService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteInfoController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
