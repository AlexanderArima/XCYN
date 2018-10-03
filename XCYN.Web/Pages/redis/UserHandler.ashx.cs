using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCYN.Web.Model;

namespace XCYN.Web.Pages.redis
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler
    {

        private XSession session;

        public void ProcessRequest(HttpContext context)
        {
            session = XSession.GetInstance();
            string UserName = context.Request["UserName"];
            if (!string.IsNullOrEmpty(UserName))
            {
                //记录登陆状态
                XUser user = new XUser()
                {
                    UserName = UserName,
                    ID = 1,
                    Age = 12
                };
                session.UserInfo = user;
                context.Response.Redirect("Index.aspx");
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