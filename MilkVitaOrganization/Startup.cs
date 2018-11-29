using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MilkVitaOrganization.Startup))]
namespace MilkVitaOrganization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
