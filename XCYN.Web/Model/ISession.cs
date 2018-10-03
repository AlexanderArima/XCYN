using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;

namespace XCYN.Web.Model
{
    interface ISession
    {
        HttpSessionState session { get; set; }
        XUser UserInfo { get; set; }
    }
}
