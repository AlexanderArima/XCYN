using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XCYN.MVC.Common;

namespace XCYN.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //在Ninject和MVC中间创建一个支持DI的桥梁
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            DependencyResolver.SetResolver(new NinjectDepResolver(new StandardKernel()));
        }
    }
}
