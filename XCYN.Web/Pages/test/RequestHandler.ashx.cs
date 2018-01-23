using System;
using System.Collections.Generic;
using System.Linq;
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
            var action = context.Request["action"];
            switch (action)
            {
                case "IsGetTest":
                    IsGetTest();
                    break;
                case "IsPostTest":
                    IsPostTest();
                    break;
                default:
                    break;
            }
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
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