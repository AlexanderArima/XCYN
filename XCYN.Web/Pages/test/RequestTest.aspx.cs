using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCYN.Common;

namespace XCYN.Web.Pages.test
{
    public partial class RequestTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
        }
    }
}