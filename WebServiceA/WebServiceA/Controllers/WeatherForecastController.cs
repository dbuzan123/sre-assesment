using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.Caching;

namespace WebServiceA.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {


        public WeatherForecastController()
        {
        }

        [HttpGet()]
        public IActionResult Get()
        {

            ObjectCache cache = MemoryCache.Default;

            double lastRate = (double)cache.LastOrDefault().Value;
            double avg = 0;

            foreach (var item in cache)
            {
                avg += (double)item.Value;
            }
            avg = avg / (double)cache.Count();

            return Ok(new {lastRate = lastRate, average = avg});
            
        }

        [HttpGet("avg")]
        public IActionResult GetAvg()
        {


            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10),

            };
            return Ok("Service A");

        }
    }
}