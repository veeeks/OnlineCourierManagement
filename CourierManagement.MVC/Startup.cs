using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourierManagement.MVC.Startup))]
namespace CourierManagement.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
