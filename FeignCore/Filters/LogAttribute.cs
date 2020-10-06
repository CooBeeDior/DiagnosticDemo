using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;


namespace FeignCore.Filters
{
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
}
