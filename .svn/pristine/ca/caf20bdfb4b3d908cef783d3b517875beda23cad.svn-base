using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(XCYN.WS.APP.Startup1))]

namespace XCYN.WS.APP
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            //调用UseCors方法可以兼顾到跨域的问题，使用前请引入Microsoft.Owin.Cors
            app.UseCors(CorsOptions.AllowAll).MapSignalR();
        }
    }
}
