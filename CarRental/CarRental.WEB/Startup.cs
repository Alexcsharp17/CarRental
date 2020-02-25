using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRental.WEB.Startup))]
namespace CarRental.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
