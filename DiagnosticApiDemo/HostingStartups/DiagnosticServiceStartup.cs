using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DiagnosticCore.Constant;
using System.Text.RegularExpressions;

namespace DiagnosticApiDemo.HostingStartups
{

    public class DiagnosticServiceStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //添加诊断跟踪
                services.AddDiagnostics(options =>
                {
                    options.SetRequestRequestRucle((request) =>
                    { 
                        if (request.Headers.ContainsKey(HttpConstant.TRACEMICROSERVICE)
                         && Regex.Match(request.Path, @"^(/v\d\.\d)?/api").Success)
                        {
                            return true;
                        }
                        return false;
                    });
                });
 
            });
        }
    }


}
