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
            
            var authenticationProviderKey = "OcelotKey";

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer(authenticationProviderKey, options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;
            //        options.Audience = "dennis.microservice.testapi-a";
            //    });


            //Action<IdentityServerAuthenticationOptions> options = o =>
            //{
            //    o.Authority = "http://localhost:5000";
            //    o.ApiName = "dennis.microservice.testapi-a";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.RequireHttpsMetadata = false;
            //};

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication(authenticationProviderKey, options);

            services.AddOcelot().AddConsul().AddPolly();

            //Action<IdentityServerAuthenticationOptions> isaOpt = option =>
            //{
            //    option.Authority = "http://localhost:5000";
            //    option.ApiName = "dennis.microservice.testapi-a";
            //    option.RequireHttpsMetadata = false;
            //    option.SupportedTokens = SupportedTokens.Both;
            //    option.ApiSecret = "ocelot-clientsecrets";
            //};
            //services.AddAuthentication().AddIdentityServerAuthentication(authenticationProviderKey, isaOpt);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            //app.UseAuthentication();
            app.UseOcelot().Wait();

        }
    }
}
