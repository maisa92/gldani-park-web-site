using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GldaniParkFront.Startup))]
namespace GldaniParkFront
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
