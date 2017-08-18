using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.EasyUI
{
    /// <summary>
    /// RegisterHandler 的摘要说明
    /// </summary>
    public class RegisterHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string name = context.Request["name"];
            string email = context.Request["email"];
            string user_name = context.Request["user_name"];
            context.Response.Write("name:" + name + "_email:" + email + "_user_name:"+ user_name);
            context.Response.End();
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