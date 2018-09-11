using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.MVC.Controllers
{
    [RoutePrefix("Admin")]
    [Route("{action=Index}")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [Route("Create/{name:alpha}/{age=18}")]
        public ContentResult Create(string name,int age)
        {
            return Content(string.Format("name:{0},age:{1}", name, age));
        }

        /// <summary>
        /// 返回一个分部视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetAR()
        {
            return PartialView("PW");
        }

    }
}