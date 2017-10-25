using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCYN.WS
{
    public partial class inspect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var connectionID = ChatHub._set.ElementAt(0).ConnectionID;
            //通过调用GlobalHost的GetHubContext可以获取到Hub的上下文
            var hub = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hub.Clients.Client(connectionID).notice("您的余额不足请充值");

        }
    }
}