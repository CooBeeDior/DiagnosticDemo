# netcore诊断
##### *HostingTracing* 
监听的方法：
- Microsoft.AspNetCore.Hosting.HttpRequestIn.Start 【Microsoft.AspNetCore.Http.DefaultHttpContext】
- Microsoft.AspNetCore.Hosting.BeginRequest 【<>f__AnonymousType0`2[[Microsoft.AspNetCore.Http.HttpContext, Microsoft.AspNetCore.Http.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Int64, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】
- Microsoft.AspNetCore.Routing.EndpointMatched 【Microsoft.AspNetCore.Http.DefaultHttpContext】
- Microsoft.AspNetCore.Mvc.BeforeAction 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionEventData】
- Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutingEventData】
- Microsoft.AspNetCore.Mvc.AfterOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutingEventData】
- Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutingEventData】
- Microsoft.AspNetCore.Mvc.AfterOnActionExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutingEventData】
- Microsoft.AspNetCore.Mvc.BeforeActionMethod 【<>f__AnonymousType0`3[[Microsoft.AspNetCore.Mvc.ActionContext, Microsoft.AspNetCore.Mvc.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Collections.Generic.IReadOnlyDictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】
- Microsoft.AspNetCore.Mvc.BeforeControllerActionMethod 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeControllerActionMethodEventData】
- Microsoft.AspNetCore.Mvc.AfterControllerActionMethod 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterControllerActionMethodEventData】
- Microsoft.AspNetCore.Mvc.AfterActionMethod 【<>f__AnonymousType1`4[[Microsoft.AspNetCore.Mvc.ActionContext, Microsoft.AspNetCore.Mvc.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Collections.Generic.IReadOnlyDictionary`2[[System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Object, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[Microsoft.AspNetCore.Mvc.IActionResult, Microsoft.AspNetCore.Mvc.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60]]】
- Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutedEventData】
- Microsoft.AspNetCore.Mvc.AfterOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutedEventData】
- Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionFilterOnActionExecutedEventData】
- Microsoft.AspNetCore.Mvc.AfterOnActionExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionFilterOnActionExecutedEventData】
- Microsoft.AspNetCore.Mvc.BeforeOnResultExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeResultFilterOnResultExecutingEventData】
- Microsoft.AspNetCore.Mvc.AfterOnResultExecuting 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterResultFilterOnResultExecutingEventData】
- Microsoft.AspNetCore.Mvc.BeforeActionResult 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeActionResultEventData】
- Microsoft.AspNetCore.Mvc.AfterActionResult 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionResultEventData】
- Microsoft.AspNetCore.Mvc.BeforeOnResultExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.BeforeResultFilterOnResultExecutedEventData】
- Microsoft.AspNetCore.Mvc.AfterOnResultExecuted 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterResultFilterOnResultExecutedEventData】
- Microsoft.AspNetCore.Mvc.AfterAction 【Microsoft.AspNetCore.Mvc.Diagnostics.AfterActionEventData】
- Microsoft.AspNetCore.Hosting.EndRequest 【<>f__AnonymousType0`2[[Microsoft.AspNetCore.Http.HttpContext, Microsoft.AspNetCore.Http.Abstractions, Version=3.1.6.0, Culture=neutral, PublicKeyToken=adb9793829ddae60],[System.Int64, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]】
- Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop 【Microsoft.AspNetCore.Http.DefaultHttpContext】

##### *HttpClientTracing*
监听的方法：
* `System.Net.Http.Request`
* `System.Net.Http.Response`
* `System.Net.Http.Exception` 

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