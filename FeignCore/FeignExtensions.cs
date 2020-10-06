using DiagnosticModel;
using FeignCore.Apis;
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
            //IApiParameterAttribute HttpContentAttribute
            //IApiReturnAttribute ApiReturnAttribute
            //IApiActionAttribute ApiActionAttribute
            //IApiActionFilter IApiActionFilterAttribute

            services.AddHttpApi<IUserApi>();


        }
    }
     
}
