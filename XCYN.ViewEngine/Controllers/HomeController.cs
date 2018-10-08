using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.ViewEngine.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Messaage = "Hello World";
            ViewBag.Time = DateTime.Now;
            return View("DebugData");
        }

        /// <summary>
        /// 使用分段Section来展示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSection()
        {
            string[] str = new string[]
            {
                "China",
                "USA",
                "France"
            };
            return View(str);
        }

        public ActionResult GetParital()
        {
            return View();
        }

        public ActionResult GetStrongResult()
        {
            return View();
        }

        /// <summary>
        /// 调用子动作
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChildAction()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Time()
        {
            return PartialView(DateTime.Now);
        }
    }
}