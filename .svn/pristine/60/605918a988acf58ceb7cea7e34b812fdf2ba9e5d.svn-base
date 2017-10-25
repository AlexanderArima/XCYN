using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCYN.EasyUI.model;

namespace XCYN.EasyUI.ashx
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            switch (action)
            {
                case "Login":
                    Login(context);
                    break;
                
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        private void Login(HttpContext context)
        {
            string username = context.Request["username"];
            string password = context.Request["password"];
            using(MeetingSysEntities db = new MeetingSysEntities())
            {
                var query = from a in db.users
                            where a.user_name.Equals(username)
                            select a;
                var count = query.Count();
                if(count > 0)
                {
                    context.Response.Clear();
                    context.Response.Write("1");
                    context.Response.End();
                }
                else
                {
                    context.Response.Clear();
                    context.Response.Write("0");
                    context.Response.End();
                }
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