using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HouseStats.Startup))]
namespace HouseStats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
