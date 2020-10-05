using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpiderCore;
using SpiderCore.RequestStrategies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace FeignCore
{
    public static class FeignExtensions
    {
        public static void AddFeign(this IServiceCollection services)
        {
            //ContextInvoker

            services.AddHttpApi<IUserApi>(o =>
            {
                o.HttpHost = new Uri("http://localhost:5000/");
            });


        }
    }

    [Service("user")]
    public interface IUserApi : IHttpApi
    {
        [HttpGet("api/users/{account}")]
        [JsonReturn]
        ITask<string> GetExpectJsonAsync([Required] string account, CancellationToken token = default);

    }
    public class OAuthTokenAttribute : ApiActionAttribute, IApiActionAttribute
    {
        public override Task BeforeRequestAsync(ApiActionContext context)
        {
            //context.GetService<>() //获取token
            context.RequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("");
            return Task.CompletedTask;
        }
    }

    public class LogAttribute : ApiActionFilterAttribute, IApiActionFilterAttribute
    {
        //
        // 摘要:
        //     准备请求之前
        //
        // 参数:
        //   context:
        //     上下文
        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            var logger = context.GetService<ILogger<LogAttribute>>();
            return Task.CompletedTask;
        }
        //
        // 摘要:
        //     请求完成之后
        //
        // 参数:
        //   context:
        //     上下文
        public override Task OnEndRequestAsync(ApiActionContext context)
        {
            var logger = context.GetService<ILogger<LogAttribute>>();
            return Task.CompletedTask;
        }
    }
    public class ServiceAttribute : ApiActionAttribute, IApiActionAttribute
    {
        public string Name { get; }
        public ServiceAttribute(string name)
        {
            this.Name = name;
        }


        public override Task BeforeRequestAsync(ApiActionContext context)
        {
            //注册中心 根据name获取url 
            var requestStrategy = RequestStrategyFactory.Instance.CreateRequestStrategy(new SpiderService(this.Name));
            string baseurl = requestStrategy.GetServiceIp();
            context.HttpApiConfig.HttpClient.BaseAddress = new System.Uri(baseurl);
            return Task.CompletedTask;
        }
    }
}
