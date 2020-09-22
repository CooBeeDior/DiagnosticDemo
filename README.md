# netcore诊断
##### *HostingTracing* 
监听的方法：
Microsoft.AspNetCore.Hosting.HttpRequestIn.Start 【Microsoft.AspNetCore.Http.DefaultHttpContext】
Microsoft.AspNetCore.Hosting.BeginRequest 【Microsoft.AspNetCore.Http.HttpContext】
Microsoft.AspNetCore.Routing.EndpointMatched 【Microsoft.AspNetCore.Http.DefaultHttpContext】
Microsoft.AspNetCore.Mvc.BeforeAction 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[HttpContext, {Microsoft.AspNetCore.Http.DefaultHttpContext}]}
{[RouteData, {Microsoft.AspNetCore.Routing.RouteData}]}

Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutingEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutingContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutingContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.ModelBinding.UnsupportedContentTypeFilter}]}

Microsoft.AspNetCore.Mvc.AfterOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutingEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutingContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutingContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.ModelBinding.UnsupportedContentTypeFilter}]}

Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutingEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutingContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutingContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.Infrastructure.ModelStateInvalidFilter}]}

Microsoft.AspNetCore.Mvc.AfterOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutingEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutingContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutingContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.Infrastructure.ModelStateInvalidFilter}]}

Microsoft.AspNetCore.Mvc.BeforeActionMethod 【<>f__AnonymousType0`3[[Microsoft.AspNetCore.Mvc.ActionContext, Microsoft.AspNetCore.Mvc.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Collections.Generic.IReadOnlyDictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】

Microsoft.AspNetCore.Mvc.BeforeControllerActionMethod 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeControllerActionMethodEventData】
{[ActionContext, {Microsoft.AspNetCore.Mvc.ControllerContext}]}
{[ActionArguments, Count = 0]}
{[Controller, {DiagnosticApiDemo.Controllers.WeatherForecastController}]}


Microsoft.AspNetCore.Mvc.AfterControllerActionMethod 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterControllerActionMethodEventData】
{[ActionContext, {Microsoft.AspNetCore.Mvc.ControllerContext}]}
{[Controller, {Microsoft.AspNetCore.Mvc.ControllerContext}]}
{[Controller, {Microsoft.AspNetCore.Mvc.ControllerContext}]}
{[Result, {Microsoft.AspNetCore.Mvc.ObjectResult}]}

Microsoft.AspNetCore.Mvc.AfterActionMethod 【<>f__AnonymousType1`4[[Microsoft.AspNetCore.Mvc.ActionContext, Microsoft.AspNetCore.Mvc.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Collections.Generic.IReadOnlyDictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[Microsoft.AspNetCore.Mvc.IActionResult, Microsoft.AspNetCore.Mvc.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]]】

Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutedEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutedContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutedContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.Infrastructure.ModelStateInvalidFilter}]}

Microsoft.AspNetCore.Mvc.AfterOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutedEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutedContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutedContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.Infrastructure.ModelStateInvalidFilter}]}

Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutedEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutedContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutedContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.ModelBinding.UnsupportedContentTypeFilter}]}


Microsoft.AspNetCore.Mvc.AfterOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutedEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ActionExecutedContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.ActionExecutedContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.ModelBinding.UnsupportedContentTypeFilter}]}

Microsoft.AspNetCore.Mvc.BeforeOnResultExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeResultFilterOnResultExecutingEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ResultExecutingContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultExecutingContextSealed}]}
{[ResultExecutingContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultExecutingContextSealed}]}

Microsoft.AspNetCore.Mvc.AfterOnResultExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterResultFilterOnResultExecutingEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ResultExecutingContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultExecutingContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.Infrastructure.ClientErrorResultFilter}]}

Microsoft.AspNetCore.Mvc.BeforeActionResult 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionResultEventData】
{[ActionContext, {Microsoft.AspNetCore.Mvc.ControllerContext}]}
{[Result, {Microsoft.AspNetCore.Mvc.ObjectResult}]}

Microsoft.AspNetCore.Mvc.AfterActionResult 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionResultEventData】
{[ActionContext, {Microsoft.AspNetCore.Mvc.ControllerContext}]}
{[Result, {Microsoft.AspNetCore.Mvc.ObjectResult}]}

Microsoft.AspNetCore.Mvc.BeforeOnResultExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeResultFilterOnResultExecutedEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ResultExecutedContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultExecutedContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.Infrastructure.ClientErrorResultFilter}]}

Microsoft.AspNetCore.Mvc.AfterOnResultExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterResultFilterOnResultExecutedEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[ResultExecutedContext, {Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultExecutedContextSealed}]}
{[Filter, {Microsoft.AspNetCore.Mvc.Infrastructure.ClientErrorResultFilter}]}

Microsoft.AspNetCore.Mvc.AfterAction 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionEventData】
{[ActionDescriptor, DiagnosticApiDemo.Controllers.WeatherForecastController.Get (DiagnosticApiDemo)]}
{[HttpContext, {Microsoft.AspNetCore.Http.DefaultHttpContext}]}
{[RouteData, {Microsoft.AspNetCore.Routing.RouteData}]}

Microsoft.AspNetCore.Hosting.EndRequest 【<>f__AnonymousType0`2[[Microsoft.AspNetCore.Http.HttpContext, Microsoft.AspNetCore.Http.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Int64, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】

Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop 【Microsoft.AspNetCore.Http.DefaultHttpContext】

##### *HttpClientTracing*
监听的方法：
- System.Net.Http.HttpRequestOut.Start 【<>f__AnonymousType0`1[[System.Net.Http.HttpRequestMessage, System.Net.Http, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]】
- System.Net.Http.Request 【<>f__AnonymousType1`3[[System.Net.Http.HttpRequestMessage, System.Net.Http, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a],[System.Guid, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Int64, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】
- System.Net.Http.HttpRequestOut.Stop 【<>f__AnonymousType3`3[[System.Net.Http.HttpResponseMessage, System.Net.Http, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a],[System.Net.Http.HttpRequestMessage, System.Net.Http, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a],[System.Threading.Tasks.TaskStatus, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】
- System.Net.Http.Response 【<>f__AnonymousType4`4[[System.Net.Http.HttpResponseMessage, System.Net.Http, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a],[System.Guid, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Int64, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Threading.Tasks.TaskStatus, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】


         ```c-sharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDiagnostics();
            services.AddControllers();
        }
		   
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
    
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAllDiagnostic();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
		```