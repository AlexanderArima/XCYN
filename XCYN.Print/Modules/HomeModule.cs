using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Hosting;

namespace XCYN.Print.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            this.Get["/"] = _ =>
            {
                return "Home Index";
            };

            this.Get["Home/"] = _ =>
            {
                return this.View["index"];
            };
        }
    }
}
