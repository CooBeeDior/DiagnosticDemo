using DiagnosticModel;
using SpiderCore;
using SpiderCore.RequestStrategies;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;
namespace FeignCore.Actions
{
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
            context.RequestMessage.Headers.Add("trace-microservice", Name);
            //context.RequestMessage.RequestUri = new System.Uri(Url.Combine(baseurl, context.RequestMessage.RequestUri.LocalPath));
            return Task.CompletedTask;
        }
    }
}
