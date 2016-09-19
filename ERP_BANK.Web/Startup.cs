using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERP_BANK.Web.Startup))]
namespace ERP_BANK.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
