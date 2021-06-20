using Microsoft.AspNetCore.Mvc;

namespace Read.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get() => "read app here";
    }
}
