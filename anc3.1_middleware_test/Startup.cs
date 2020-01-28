using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace anc3._1_middleware_test
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
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                Debug.WriteLine("1st middleware before next invoke message.");
                await next.Invoke();
                Debug.WriteLine("1st middleware after next invoke message.");

            });

            app.Run(async context =>
            {
                Debug.WriteLine("2nd middleware before next invoke message.");
                await context.Response.WriteAsync("Hello from 2nd delegate.");
                Debug.WriteLine("2nd middleware after next invoke message.");
            });

            app.Use(async (context, next) =>
            {
                Debug.WriteLine("3rd middleware before next invoke message.");
                await next.Invoke();
                Debug.WriteLine("3rd middleware after next invoke message.");

            });
        }
    }
}
