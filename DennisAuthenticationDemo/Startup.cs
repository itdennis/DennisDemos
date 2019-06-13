using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DennisAuthenticationDemo.Startup))]
namespace DennisAuthenticationDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
