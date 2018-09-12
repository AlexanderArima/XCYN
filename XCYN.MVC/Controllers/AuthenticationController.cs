using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace XCYN.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult Login(string UserName,string Password)
        {
            if (UserName.Contains("xc"))
            {
                FormsAuthentication.SetAuthCookie(UserName, true);
                return Redirect(FormsAuthentication.LoginUrl);
            }
            else
            {
                return Redirect("Login");
            }
        }
    }
}