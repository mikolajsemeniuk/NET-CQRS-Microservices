using Microsoft.AspNetCore.Mvc;

namespace Write.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get() => "write app here";
    }
}
