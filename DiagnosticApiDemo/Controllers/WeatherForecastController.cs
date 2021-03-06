﻿using System;
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
using TransPortServiceAbstraction;
using DiagnosticModel;
using DiagnosticCore;
using SpiderCore;
using StackExchange.Profiling;
using MongodbCore;
using System.Globalization;
using FeignCore;
using FeignCore.Apis;
using WebApiClient;
using ProtoBuf;
using System.Net.Http.Headers;
using ProtobufCore;

namespace DiagnosticApiDemo.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISpiderHttpClientFactory _spiderHttpClientFactory;
        private readonly SpiderOptions _spiderOptions;
        private readonly IUserApi _userApi;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, Func<string, ITransPortService> func,
            IStringLocalizer<WeatherForecastController> stringLocalizer, IHttpContextAccessor httpContextAccessor,
            ISpiderHttpClientFactory spiderHttpClientFactory, SpiderOptions spiderOptions, IUserApi userApi)
        {
            _logger = logger;
            _userApi = userApi;
            _httpContextAccessor = httpContextAccessor;

            _spiderHttpClientFactory = spiderHttpClientFactory;
            _spiderOptions = spiderOptions;

            var mongodb = func.Invoke(MongodbConstant.MONGODBNAME);
            var dad = CultureInfo.CurrentUICulture;
            var c = stringLocalizer["test"];
            var activity = System.Diagnostics.Activity.Current;
            activity?.AddTag("1", "1");
            activity?.AddBaggage("1", "1");
        }



        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        { 
            var activity = System.Diagnostics.Activity.Current;
            activity?.AddTag("2", "2");
            activity?.AddBaggage("2", "2");
            var res = await _userApi.GetQrCode().Retry(3);

            string url1 = string.Empty;
            string url2 = string.Empty;
            using (MiniProfiler.Current.Step("Get方法"))
            {
                using (MiniProfiler.Current.Step("准备数据"))
                {
                    using (MiniProfiler.Current.CustomTiming("SQL", "SELECT * FROM Config"))
                    {
                        // 模拟一个SQL查询
                        Thread.Sleep(500);

                        url1 = "https://www.baidu.com";
                        url2 = "https://www.sina.com.cn/";
                    }
                }


                using (MiniProfiler.Current.Step("使用从数据库中查询的数据，进行Http请求"))
                {
                    using (MiniProfiler.Current.CustomTiming("HTTP", "GET " + url1))
                    {
                        var client = new HttpClient();
                        var reply = await client.GetAsync(url1);
                    }

                    using (MiniProfiler.Current.CustomTiming("HTTP", "GET " + url2))
                    {
                        var client = new HttpClient();
                        var reply = await client.GetAsync(url2);
                    }
                }
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("options")]
        public async Task<string> Options()
        {

            //_spiderOptions.HealthUrl = "/ishealth";
            //_spiderOptions.Services.Add(new SpiderService("service1") { });
            var cc = _spiderOptions.Services.Where(o => o.ServiceName == "wechat").FirstOrDefault();

            cc.ServiceEntryies.Add(new SpiderServiceEntry("http://coobeedior.com:8012/"));
            return "124";
        }

        [HttpGet("baidu")]
        public async Task<string> BaiDu()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));
            var response = await client.GetAsync("http://localhost:5000/api/weatherforecast");
            var result1 = await response.Content.ReadAsAsync<IEnumerable<WeatherForecast>>(new[] { new ProtoBufFormatter() });


            _logger.LogInformation("请求百度");
            //var resp =await _spider.GetAsync("http://www.baidu.com");
            var resp = await _spiderHttpClientFactory.CreateSpiderHttpClient("wechat").PostAsync("/api/Login/GetQrCode");

            //var r1esp = await _spiderHttpClientFactory.CreateSpiderHttpClient("name").PostAsync("/api/Login/GetQrCode");
            var result = await resp.Content.ReadAsStringAsync();
            _logger.LogInformation("请求百度结束11");


            return result;
        }


        [HttpPost("baidu")]
        public async Task<string> BaiDuPost()
        {

            var resp = await _spiderHttpClientFactory.CreateSpiderHttpClient("wechat").PostAsync("/api/Login/GetQrCode");
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
    [Route("api/WeatherForecast")]
    public class WeatherForecast2Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecast2Controller(ILogger<WeatherForecastController> logger, Func<string, ITransPortService> func)
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
