using Microsoft.AspNetCore.Mvc;

namespace MyBackendApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        // GET: api/hello
        [HttpGet]
        public IActionResult GetHelloMessage()
        {
            return Ok("Hello, world!");
        }

        // GET: api/hello/{name}
        [HttpGet("{name}")]
        public IActionResult GetPersonalizedMessage(string name)
        {
            return Ok($"Hello, {name}!");
        }
    }
}
