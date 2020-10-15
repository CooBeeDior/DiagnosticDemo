using DiagnosticCore;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using ProtobufCore;
using SpiderCore.RequestStrategies;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace DiagnosticApiDemo
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {  // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
                options.InputFormatters.Add(new ProtobufInputFormatter());
                options.OutputFormatters.Add(new ProtobufOutputFormatter());
                //Header Accept:application/x-protobuf
                options.FormatterMappings.SetMediaTypeMappingForFormat("protobuf", MediaTypeHeaderValue.Parse("application/x-protobuf"));
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseCors("cors");// 启用CORS服务
            //添加诊断
            app.UseDiagnostics();

            //添加消息队列
            app.UseRabbitmq();

            //  /profiler/results
            app.UseMiniProfiler();

            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点 {url}/swagger
            app.UseSwaggerUI(options =>
            {
                options.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream($"DiagnosticApiDemo.wwwroot.index.html");
                var provider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                options.DefaultModelsExpandDepth(-1); //设置为 - 1 可不显示models
                //options.DocExpansion(DocExpansion.List );//设置为none可折叠所有方法
            });



            app.UseHangfireServer();//启动Hangfire服务
            app.UseHangfireDashboard();//启动hangfire面板 {url}/hangfire
            //app.UseMonitorHealthJob(); //启动服务健康检查

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                //英文
                new CultureInfo("en-US"),
                //中文
                new CultureInfo("zh-CN"),
            };
            //添加本地化机制
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
              
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            app.UseRouting();

         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
