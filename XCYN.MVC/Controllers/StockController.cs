using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.MVC.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取保存静态页面绝对路径
        /// </summary>        
        /// <returns></returns>
        private string GetStaticPageAbsolutePath()
        {
            //静态页面名称
            string strStaticPageName = string.Format("{0}.html", DateTime.Now.Ticks.ToString());
            //静态页面相对路径
            string strStaticPageRelativePath = string.Format("article\\{0}\\{1}", DateTime.Now.ToString("yyyy/MM").Replace('/', '\\'), strStaticPageName);
            //静态页面完整路径                                    
            string strStaticPageAbsolutePath = AppDomain.CurrentDomain.BaseDirectory + strStaticPageRelativePath;
            return strStaticPageAbsolutePath;
        }
    }
}