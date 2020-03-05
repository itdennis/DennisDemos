using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;

namespace mvc_client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies"; //We are using a cookie to locally sign-in the user (via "Cookies" as the DefaultScheme)
                options.DefaultChallengeScheme = "oidc"; //we set the DefaultChallengeScheme to oidc because when we need the user to login, we will be using the OpenID Connect protocol.
            })
                .AddCookie("Cookies") //We then use AddCookie to add the handler that can process cookies.
                .AddOpenIdConnect("oidc", options => //used to configure the handler that perform the OpenID Connect protocol.
                {
                    options.Authority = "http://localhost:5000"; //The Authority indicates where the trusted token service is located.
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvc"; // identify this client via the ClientId and the ClientSecret.
                    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                    options.ResponseType = "code";

                    options.SaveTokens = true; //SaveTokens is used to persist the tokens from IdentityServer in the cookie (as they will be needed later).Since SaveTokens is enabled, ASP.NET Core will automatically store the resulting access and refresh token in the authentication session. 

                    options.Scope.Add("api1");
                    options.Scope.Add("offline_access");
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization(); //The RequireAuthorization method disables anonymous access for the entire application.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
