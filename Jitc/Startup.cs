using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jitc.Startup))]
namespace Jitc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
