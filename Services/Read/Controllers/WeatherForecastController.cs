using Microsoft.AspNetCore.Mvc;

namespace Read.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get() => "read app here";
    }
}
