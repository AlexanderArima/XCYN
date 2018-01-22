using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCYN.Common;

namespace XCYN.Web.Pages.webform
{
    public partial class PostList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = DbHelperSQL.GetDataTable("SELECT TOP 10 TypeID,TypeName,TypePrice,AddBedPrice,IsAddBed,Remark FROM RoomType");
            GridView1.DataBind();
        }
    }
}