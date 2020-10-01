using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PersistenceAbstraction;
using DiagnosticModel;
using DiagnosticCore;
using SpiderCore;

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
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISpiderHttpClientFactory _spiderHttpClientFactory;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, Func<string, IPersistence> func,
            IStringLocalizer<WeatherForecastController> stringLocalizer, IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor,  
            ISpiderHttpClientFactory spiderHttpClientFactory)
        {
            _logger = logger;
            var c = stringLocalizer["ddd"];
            _httpContextAccessor = httpContextAccessor;
            _httpClient = clientFactory.CreateClient("aaa");
            _spiderHttpClientFactory = spiderHttpClientFactory;
        }

        private Task doAysnc()
        {
            return Task.Factory.StartNew(() =>
            {
                id = Thread.CurrentThread.ManagedThreadId;
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var httpContext = _httpContextAccessor.HttpContext;

                System.IO.File.AppendAllLines("d:/f.txt", new List<string>() { Thread.CurrentThread.CurrentCulture.ToJson() });
            });
        }
        int id = 0;

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var id2 = Thread.CurrentThread.ManagedThreadId;
            var httpContext = _httpContextAccessor.HttpContext;
            await doAysnc().ConfigureAwait(false);
            var id3 = Thread.CurrentThread.ManagedThreadId;
            var httpCont1ext = _httpContextAccessor.HttpContext;

            System.IO.File.AppendAllLines("d:/d.txt", new List<string>() { $"{id}--{id2}--{id3}" });
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
            _logger.LogInformation("请求百度");
            //var resp =await _spider.GetAsync("http://www.baidu.com");
            var resp = await _spiderHttpClientFactory.CreateSpiderHttpClient("aaa").PostAsync("/api/Login/GetQrCode");
            var result = await resp.Content.ReadAsStringAsync();
            _logger.LogInformation("请求百度结束11");
   

            return result;
        }


        [HttpPost("baidu")]
        public async Task<string> BaiDuPost()
        {

            var resp = await _httpClient.GetAsync("http://www.baidu.com");
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
        public class Student
        {

            public string Id { get; set; }
            public string Name { get; set; }
        }

        [HttpGet("baidu")]
        public async Task<string> BaiDu(string id)
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("http://www.baidu.com");
            var result = await resp.Content.ReadAsStringAsync();
            return result;
        }


        [HttpPost("baidu")]
        public async Task<string> BaiDuPost(Student student)
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
