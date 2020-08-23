using Microsoft.AspNetCore.Mvc;

namespace maingoframe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DontClickController : ControllerBase
    {
        [HttpGet]
        public static string Get()
        {
            return "Your browser information is now stored onto the server.";
        }
    }
}