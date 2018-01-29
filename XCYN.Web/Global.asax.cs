using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XCYN.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //this.AddOnBeginRequestAsync((o, e, a, ob)=>{
            //    return null;
            //},(r)=> {

            //});
            //this.BeginRequest += MvcApplication_BeginRequest;
        }

        //private void MvcApplication_BeginRequest(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
