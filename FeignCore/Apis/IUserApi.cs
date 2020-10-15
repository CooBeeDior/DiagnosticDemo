using FeignCore.Actions;
using FeignCore.Filters;
using System.Threading;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
namespace FeignCore.Apis
{
    [HttpHost("http://api.coobeedior.com")]
    [Service("wechat")]
    [Log]
    public interface IUserApi : IHttpApi
    {
        [HttpPost("/api/Login/GetQrCode")]
        [JsonReturn]
        ITask<object> GetQrCode(CancellationToken token = default);

    }
}
