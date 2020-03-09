using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using IdentityServer4.AccessTokenValidation;

namespace Ocelot.Core
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            
            //var authenticationProviderKey = "dennis.microservice.testapi-a";
            //Action<IdentityServerAuthenticationOptions> options = o =>
            //{
            //    o.Authority = "http://localhost:5000";
            //    o.ApiName = "dennis.microservice.testapi-a";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.RequireHttpsMetadata = false;
            //    o.ApiSecret = "";
            //};

            //var authenticationProviderKey2 = "Dennis.Blog.WebApiService";
            //Action<IdentityServerAuthenticationOptions> options2 = o =>
            //{
            //    o.Authority = "http://localhost:5000";
            //    o.ApiName = "Dennis.Blog.WebApiService";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.RequireHttpsMetadata = false;
            //    o.ApiSecret = "";
            //};

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication(authenticationProviderKey, options)
            //    .AddIdentityServerAuthentication(authenticationProviderKey2, options2);

            services.AddOcelot().AddConsul().AddPolly();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            //app.UseAuthentication();
            app.UseOcelot().Wait();

        }
    }
}
