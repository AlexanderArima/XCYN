using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCYN.Common.Sql.redis;
using XCYN.Web.Model;

namespace XCYN.Web.Pages.redis
{
    public partial class Login : System.Web.UI.Page
    {
        public string UserName = "";
        private XSession session;

        protected void Page_Load(object sender, EventArgs e)
        {
            session = XSession.GetInstance();
            //判断用户是否登陆
           // Session.SessionID;
            if(session.UserInfo.UserName != null)
            {
                //登陆完成，跳转到Index页面
                Context.Response.Redirect("Index.aspx");
            }
        }
    }
}