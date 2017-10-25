using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace XCYN.EasyUI
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Thread.Sleep(1000);
            context.Response.ContentType = "text/plain";
            if (context.Request["username"].Equals("xc"))
            {
                context.Response.Write("true");
            }
            else
            {
                context.Response.Write("false");
            }
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