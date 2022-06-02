using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tiger_Web.Startup))]
namespace Tiger_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
