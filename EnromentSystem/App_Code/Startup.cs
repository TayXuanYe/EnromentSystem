using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnromentSystem.Startup))]
namespace EnromentSystem
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
