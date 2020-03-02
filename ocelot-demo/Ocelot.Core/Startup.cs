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
            services.AddOcelot().AddConsul().AddPolly();
            var authenticationProviderKey = "TestKey";

            Action<IdentityServerAuthenticationOptions> isaOpt = option =>
            {
                option.Authority = _config["IdentityService:Uri"];
                option.ApiName = "clientservice";
                option.RequireHttpsMetadata = Convert.ToBoolean(_config["IdentityService:UseHttps"]);
                option.SupportedTokens = SupportedTokens.Both;
                option.ApiSecret = _config["IdentityService:ApiSecrets:clientservice"];
            };
            services.AddAuthentication().AddIdentityServerAuthentication(authenticationProviderKey, isaOpt);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseOcelot().Wait();

        }
    }
}
