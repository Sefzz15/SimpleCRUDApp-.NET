using Microsoft.AspNetCore.Mvc;

namespace MyBackendApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult GetRootMessage()
        {
            return Ok("Hello from the root API endpoint!");
        }
    }
}
