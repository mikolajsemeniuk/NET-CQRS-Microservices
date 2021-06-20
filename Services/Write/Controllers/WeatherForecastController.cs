using Microsoft.AspNetCore.Mvc;

namespace Write.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get() => "write app here";
    }
}
