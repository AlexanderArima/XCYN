using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            var date = DateTime.Now;
            if(date.Hour <= 11)
            {
                ViewBag.Greeting = "Good Morning";
            }
            else if(date.Hour > 11 && date.Hour < 18)
            {
                ViewBag.Greeting = "Good Afternoon";
            }
            else if(date.Hour < 24)
            {
                ViewBag.Greeting = "Good Evening";
            }
            return View();
        }

        public RedirectResult Redir()
        {
            return Redirect("Index");
        }

        public HttpUnauthorizedResult UnAuth()
        {
            return new HttpUnauthorizedResult();
        }
    }
}