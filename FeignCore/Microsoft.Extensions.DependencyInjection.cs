using FeignCore.Apis;
using WebApiClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FeignExtensions
    {
        public static void AddFeign(this IServiceCollection services)
        {
            //ContextInvoker  
            //IApiParameterAttribute HttpContentAttribute
            //IApiReturnAttribute ApiReturnAttribute
            //IApiActionAttribute ApiActionAttribute
            //IApiActionFilter IApiActionFilterAttribute

            services.AddHttpApi<IUserApi>();


        }
    }
     
}
