using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreeterController : ControllerBase
    {
        [HttpGet]
        public string Get(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Hello anonymous";
            }

            return $"Hello {name}";
        }
    }
}