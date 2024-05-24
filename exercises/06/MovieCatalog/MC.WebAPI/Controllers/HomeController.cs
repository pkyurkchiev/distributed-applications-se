using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class HomeController : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [HttpGet]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Get()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return "Hello World!";
        }

        [HttpGet("{name}")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Get([FromRoute] string name) 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return "Hello " + name;
        }

        [HttpGet("{name}/hello")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Get([FromRoute] string name, [FromQuery] int? counter)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            StringBuilder sb = new();
            for (int i = 0; i < counter; i++)
            {
                sb.AppendLine($"Hello {name}!");
            }

            return sb.ToString();
        }

        [HttpGet("rectangle-calculator")]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IActionResult Get(double a = 0, double b = 0)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Ok(new Rectangle(a, b));
        }
    }
}
