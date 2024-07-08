using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.WebApi.Nancy.Modules
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        /// <summary>
        /// 启动面板.
        /// </summary>
        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = @"123456" }; }
        }
    }
}