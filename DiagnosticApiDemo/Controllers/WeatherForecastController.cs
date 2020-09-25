using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersistenceAbstraction;

namespace DiagnosticApiDemo.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,Func<string, IPersistence> func)
        {
            _logger = logger;
 
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
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

        [HttpPost("exception")]
        public async Task<string> Exception()
        {
            throw new Exception("手动错误");        
        }

        [HttpPut("baidu")]
        public async Task<string> BaiDuPut()
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("http://www.baidu.com");
            var result = await resp.Content.ReadAsStringAsync();
            return "123";
        }
    }



    /// <summary>
    ///  请求方式1： weatherforecast?api-version=2.0
    ///  请求方式2: [Route("{v:apiVersion}/weatherforecast")]  2.0/weatherforecast
    ///  请求方式3:  o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");  request.header("x-api-version","2.0")   
    /// </summary>
    [ApiVersion("2.0")]
    [ApiController]
    [Route("WeatherForecast")]
    public class WeatherForecast2Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecast2Controller(ILogger<WeatherForecastController> logger, Func<string, IPersistence> func)
        {
            _logger = logger;

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
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

        [HttpPost("exception")]
        public async Task<string> Exception()
        {
            throw new Exception("手动错误");
        }

        [HttpPut("baidu")]
        public async Task<string> BaiDuPut()
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("http://www.baidu.com");
            var result = await resp.Content.ReadAsStringAsync();
            return "123";
        }
    }
}
