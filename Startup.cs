using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JewelryGolden.Startup))]
namespace JewelryGolden
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
