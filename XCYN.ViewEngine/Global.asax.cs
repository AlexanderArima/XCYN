using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XCYN.ViewEngine.Models;

namespace XCYN.ViewEngine
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册视图引擎
            ViewEngines.Engines.Add(new DebugDataViewEngine());
        }
    }
}
