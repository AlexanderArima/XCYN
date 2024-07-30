using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.MVC202406.Models;

namespace XCYN.MVC202406.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ViewData["One"] = "通过弱类型ViewData['']的方式传递数据";
            ViewBag.Two = "通过动态类型ViewBag传递数据";
            User user = new User { Name = "通过强类型ViewData.Model的方式传递数据" };
            TempData["Four"] = "通过TempData传递数据";
            return View(user);
        }
    }
}