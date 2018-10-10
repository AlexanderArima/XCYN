using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCYN.Web.Model;

namespace XCYN.Web.Pages.redis
{
    public partial class Index : System.Web.UI.Page
    {

        public string UserName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            var User = Session["UserInfo"] as XUser;
            if(User == null)
            {
                Response.Redirect("Login.aspx");
            }
            UserName = User.UserName;
        }
    }
}