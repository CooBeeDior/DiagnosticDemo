# netcore诊断
##### *HostingTracing* 
监听的方法：

- `Microsoft.AspNetCore.Hosting.HttpRequestIn.Start`  
- `Microsoft.AspNetCore.Hosting.BeginRequest`  
- `Microsoft.AspNetCore.Routing.EndpointMatched`  
- `Microsoft.AspNetCore.Mvc.BeforeAction`  
- `Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting`  
- `Microsoft.AspNetCore.Mvc.AfterOnActionExecuting`
- `Microsoft.AspNetCore.Mvc.BeforeOnActionExecuting ` 
- `Microsoft.AspNetCore.Mvc.AfterOnActionExecuting`  
- `Microsoft.AspNetCore.Mvc.BeforeActionMethod  `
- `Microsoft.AspNetCore.Mvc.BeforeControllerActionMethod`
- `Microsoft.AspNetCore.Mvc.AfterControllerActionMethod `
- `Microsoft.AspNetCore.Mvc.AfterActionMethod  `
- `Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted `
- `Microsoft.AspNetCore.Mvc.AfterOnActionExecuted  `
- `Microsoft.AspNetCore.Mvc.BeforeOnActionExecuted  `
- `Microsoft.AspNetCore.Mvc.AfterOnActionExecuted  `
- `Microsoft.AspNetCore.Mvc.BeforeOnResultExecuting ` 
- `Microsoft.AspNetCore.Mvc.AfterOnResultExecuting ` 
- `Microsoft.AspNetCore.Mvc.BeforeActionResult  `
- `Microsoft.AspNetCore.Mvc.AfterActionResult  `
- `Microsoft.AspNetCore.Mvc.BeforeOnResultExecuted ` 
- `Microsoft.AspNetCore.Mvc.AfterOnResultExecuted  `
- `Microsoft.AspNetCore.Mvc.AfterAction  `
- `Microsoft.AspNetCore.Hosting.EndRequest  `
- `Microsoft.AspNetCore.Hosting.HttpRequestIn.Stop  `

##### *HttpClientTracing*
监听的方法：

- `System.Net.Http.HttpRequestOut.Start`
- `System.Net.Http.Request`
- `System.Net.Http.HttpRequestOut.Stop`  
- `System.Net.Http.Response`  


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