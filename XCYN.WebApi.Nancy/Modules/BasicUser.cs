using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.WebApi.Nancy.Modules
{
    /// <summary>
    /// 登录用户.
    /// </summary>
    public class BasicUser
    {
        public static BasicUser user;

        public string UserName { get; set; }
    }
}