using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.MVC.Common;

namespace XCYN.MVC.App_Start
{
    public class NinjectWebCommon
    {
        private static void RegisterServices(IKernel kernel)
        {
            DependencyResolver.SetResolver(new NinjectDepResolver(kernel));
        }
    }
}