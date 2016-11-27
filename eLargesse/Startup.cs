using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eLargesse.Startup))]
namespace eLargesse
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
