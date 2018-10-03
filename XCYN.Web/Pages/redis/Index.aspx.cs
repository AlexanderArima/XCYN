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
        private ISession session = XSession.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            var User = session.UserInfo;
            UserName = User.UserName;
        }
    }
}