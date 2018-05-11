using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCYN.Web.Pages.test
{
    public partial class CacheTest5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.Response.AddHeader("Last-Modified", DateTime.Now.ToString("U", DateTimeFormatInfo.InvariantInfo));
            DateTime IfModifiedSince;
            if (DateTime.TryParse(this.Request.Headers.Get("If-Modified-Since"), out IfModifiedSince))
            {
                if ((DateTime.Now - IfModifiedSince.AddHours(8)).Seconds < 10)
                {
                    Response.Status = "304 Not Modified";
                    Response.StatusCode = 304;
                    return;
                }
            }
            Thread.Sleep(1000);
        }
    }
}