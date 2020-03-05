using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JavaScriptClient
{
    /// <summary>
    /// npm i oidc-client
    /// copy node_modules\oidc-client\dist\* wwwroot
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // Given that this project is designed to run client-side, all we need ASP.NET Core to do is to serve up the static HTML and JavaScript files that will make up our application.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();
            app.UseStaticFiles(); //This middleware will now serve up static files from the application’s ~/wwwroot folder. you need to create it by yourself.
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
