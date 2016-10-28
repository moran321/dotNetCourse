using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PriceCompare.WebClient.Startup))]
namespace PriceCompare.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
