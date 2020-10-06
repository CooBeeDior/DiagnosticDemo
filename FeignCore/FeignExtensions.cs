using DiagnosticModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpiderCore;
using SpiderCore.RequestStrategies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.AuthTokens;
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

    [Service("wechat")]
    [Log]
    public interface IUserApi : IHttpApi
    {
        [HttpPost("/api/Login/GetQrCode")]
        //[JsonReturn]
        Task<object> GetQrCode(CancellationToken token = default);

    }
    public class OAuthTokenAttribute : AuthTokenFilter
    {
        protected override Task<TokenResult> RequestRefreshTokenAsync(string refresh_token)
        {
            throw new NotImplementedException();
        }

        protected override Task<TokenResult> RequestTokenResultAsync()
        {
            throw new NotImplementedException();
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
            var options = context.GetService<SpiderOptions>();
            var spiderService = options.Services.Where(o => o.ServiceName == this.Name).FirstOrDefault();
            //注册中心 根据name获取url 
            var requestStrategy = RequestStrategyFactory.Instance.CreateRequestStrategy(spiderService);
            string baseurl = requestStrategy.GetServiceIp();
            context.HttpApiConfig.HttpHost = new System.Uri(baseurl);
 
            context.RequestMessage.RequestUri= new System.Uri(Url.Combine(baseurl, context.RequestMessage.RequestUri.LocalPath));
            return Task.CompletedTask;
        }
    }
}
