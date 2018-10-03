using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XCYN.Common;
using XCYN.Common.Performance;

namespace XCYN.Web.Pages.webform
{
    public partial class RoomEdit : System.Web.UI.Page
    {
        public int id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ////获取缓存
            //var user = CacheHelper.Get("user");
            ////清除缓存
            //CacheHelper.Remove("user");
            //获取缓存
            var user = CacheHelperBeta.Get("user");
            //清除缓存
            CacheHelperBeta.Remove("user");
        }
    }
}