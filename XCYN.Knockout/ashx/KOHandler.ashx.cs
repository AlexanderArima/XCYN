using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace XCYN.Knockout.ashx
{
    /// <summary>
    /// KOHandler 的摘要说明
    /// </summary>
    public class KOHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Thread.Sleep(10000);
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}