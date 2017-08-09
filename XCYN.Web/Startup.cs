using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XCYN.Web.Startup))]
namespace XCYN.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
