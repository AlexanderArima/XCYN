using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCYN.Web.Pages.webform
{
    public partial class RoutePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var name = Page.RouteData.Values["name"];
            var id = Page.RouteData.Values["id"];
        }
    }
}