using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        #region 各种返回结果

        /// <summary>
        /// 返回一个分部视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult GetAR()
        {
            return PartialView("PW");
        }

        /// <summary>
        /// 重定向到路由
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult RedirToAction()
        {
            //可以转到指定的控制器中的动作方法
            return RedirectToAction("Index","Home");
        }

        public RedirectToRouteResult RedirToActionPermanent()
        {
            //可以转到指定的控制器中的动作方法
            return RedirectToActionPermanent("Index", "Home");
        }

        public RedirectToRouteResult RedirToRoute()
        {
            //定义一个路由对象来重定向
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        /// <summary>
        /// 重定向到指定页面，通Response.Redirect
        /// </summary>
        /// <returns></returns>
        public RedirectResult RedirURL()
        {
            return Redirect("http://www.biying.com");
        }

        /// <summary>
        /// 返回指定文本内容，可以指定文本类型和编码格式
        /// </summary>
        /// <returns></returns>
        public ContentResult Content()
        {
            return Content("你好，中国", "text/html;text/xc", Encoding.Default);
        }

        /// <summary>
        /// 返回文件的路径
        /// </summary>
        /// <returns></returns>
        public FilePathResult FilePath()
        {
            return File("~/image/123.png", "image/png");
        }

        /// <summary>
        /// 返回图片(byte数组)
        /// </summary>
        /// <returns></returns>
        public async Task<FileContentResult> FileContent()
        {
            using (FileStream stream = new FileStream(Server.MapPath("~/image/123.png") , FileMode.Open))
            {
                byte[] b = new byte[stream.Length];
                if(await stream.ReadAsync(b, 0, b.Length) != 0)
                {
                    return File(b, "image/png");
                }
                else
                {
                    throw new Exception("无法读取图片");
                }
            }
        }
        
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <returns></returns>
        public FilePathResult FilePath2()
        {
            return File("~/image/123.png", "image/png","456.png");
        }

        /// <summary>
        /// 返回JSON数据
        /// </summary>
        /// <returns></returns>
        public JsonResult Json()
        {
            return Json(new {
                Name = "Jack",
                Age = 18
            },JsonRequestBehavior.AllowGet);
        }

        public JavaScriptResult JavaScript()
        {
            return JavaScript("<script type='text/javascript'>alert('hello world')</script>");
        }

        public HttpUnauthorizedResult Unauthorized()
        {
            return new HttpUnauthorizedResult();
        }

        #endregion


    }
}