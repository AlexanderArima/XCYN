using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using XCYN.Common;

namespace XCYN.Web.Pages.test
{
    /// <summary>
    /// RequestHandler 的摘要说明
    /// </summary>
    public class RequestHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var s1 = RequestHelper.GetServerString(RequestHelper.ServerVar.ALL_HTTP);
            var action = context.Request["action"];
            switch (action)
            {
                case "IsGetTest":
                    IsGetTest();
                    break;
                case "IsPostTest":
                    IsPostTest();
                    break;
                case "GetJS":
                    GetJS(context);
                    break;
                default:
                    break;
            }
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public void GetJS(HttpContext context)
        {
            Thread.Sleep(5000);
            context.Response.ContentType = "application/json";
            context.Response.Write(@"function Hello()
            {
                alert(""hello world"");
            }");
        }

        public void IsGetTest()
        {
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
        }

        public void IsPostTest()
        {
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
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