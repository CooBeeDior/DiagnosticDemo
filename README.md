# netcore诊断
##### *HostingTracing* 
监听的方法：
- `Microsoft.AspNetCore.Hosting.BeginRequest`
- `Microsoft.AspNetCore.Hosting.EndRequest`
- `Microsoft.AspNetCore.Diagnostics.UnhandledException`
- `Microsoft.AspNetCore.Hosting.UnhandledException`
- `Microsoft.AspNetCore.Mvc.BeforeAction`
- `Microsoft.AspNetCore.Mvc.AfterAction`

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