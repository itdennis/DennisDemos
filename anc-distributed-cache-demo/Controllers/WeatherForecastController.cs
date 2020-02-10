using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace anc_distributed_cache_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMemoryCache memoryCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            string key = "WeatherForecast_Key";
            IEnumerable<WeatherForecast> obj;
            if (!memoryCache.TryGetValue<IEnumerable<WeatherForecast>>(key, out obj))
            {
                var rng = new Random();
                obj = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToArray();
                MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddSeconds(30);
                cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                memoryCache.Set<IEnumerable<WeatherForecast>>(key, obj, cacheExpirationOptions);
            }
            return obj;
        }
    }
}
