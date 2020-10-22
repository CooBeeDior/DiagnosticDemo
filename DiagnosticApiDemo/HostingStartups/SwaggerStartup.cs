using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

                    //添加xml说明文档
                    var basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                    string xmlPath = Path.Combine(basePath, $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml");
                    options.IncludeXmlComments(xmlPath);

                    options.OperationFilter<SwaggerDefaultValues>();

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

                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                        {
                            Title = $"Swagger v{description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = "多版本管理（点右上角版本切换）<br/>",
                            Contact = new OpenApiContact() { Name = "coobeedior", Email = "coobeedior@163.com", Url = new Uri("http://coobeedior.com") }

                        });
                    }


                });

                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
            });
        }


    }
    public class SwaggerStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {

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
                next(app);
            };
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters.OfType<OpenApiParameter>())
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata.Description;
                }
                if (parameter.Example == null)
                {
                    parameter.Example = new OpenApiString(description.DefaultValue?.ToString());
                }
 

                //if (parameter.Default == null)
                //{
                //    parameter.Default = description.RouteInfo.DefaultValue;
                //}
                //parameter.Required |= !description.RouteInfo.IsOptional;

            }
        }
    }






}

