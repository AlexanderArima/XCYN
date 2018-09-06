using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using XCYN.MVC.Common;

namespace XCYN.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //,new { controller = "^H.*" ,action = "^Index$|^About$",id = new RangeRouteConstraint(10,20)}  //约束Controller只能以H打头，Action必须是Index或者About，ID的值在10-20之间
            );

            

            //两种方法都可以创建一个路由行为
            //Route route = new Route("{action}/{controller}", new MvcRouteHandler());
            //routes.Add("route1",route);

        }
    }
}
