using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiagnosticApiDemo.HostingStartups
{
    public class SwaggerStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "诊断Api",
                        Description = "诊断监听的服务",

                    });
                    // 为 Swagger JSON and UI设置xml文档注释路径
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "请输入Token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                    });
                    var openApiSecurityRequirement = new OpenApiSecurityRequirement();
                    openApiSecurityRequirement.Add(new OpenApiSecurityScheme() { Scheme = "Header", Name = "Authorization" }, new List<string>());
                    options.AddSecurityRequirement(openApiSecurityRequirement);
                    //Json Token认证方式，此方式为全局添加
                    //options.AddSecurityRequirement( );
                });
            });
        }
    }
}

