using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiagnosticApiDemo.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //throw new Exception("dd");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("baidu")]
        public async Task<string> BaiDu()
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("http://www.baidu.com");
            var result = await resp.Content.ReadAsStringAsync();
            return result;
        }


        [HttpPost("baidu")]
        public async Task<string> BaiDuPost()
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("http://www.baidu.com");
            var result = await resp.Content.ReadAsStringAsync();
            return result;
        }

        [HttpPut("baidu")]
        public async Task<string> BaiDuPut()
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("http://www.baidu.com");
            var result = await resp.Content.ReadAsStringAsync();
            return result;
        }
    }
}
