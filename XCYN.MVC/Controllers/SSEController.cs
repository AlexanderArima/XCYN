using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace XCYN.MVC.Views.SSE
{
    public class SSEController : Controller
    {
        public int LastEventID { get; set; }

        /// <summary>
        /// 主页控制器.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 服务器往客户端推送消息的服务.
        /// </summary>
        /// <returns></returns>
        public ContentResult SentNotice()
        {
            try
            {
                HttpContext.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Response.ContentType = "text/event-stream; charset=utf-8";  // 设置报文头为text/event-stream
                HttpContext.Response.Headers["Cache-control"] = "no-cache";  // 规定不对页面进行缓存
                HttpContext.Response.Headers["Keep-alive"] = "timeout=5";
                HttpContext.Response.Expires = -1;
                HttpContext.Response.StatusCode = 200;
                for (int i = 0; ;)
                {
                    try
                    {
                        HttpContext.Response.Write(string.Format("id:11\nevent:message\ndata:{0}\n\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        Response.Flush();
                    }
                    catch
                    {

                    }

                    Thread.Sleep(3000);
                }
            }
            catch
            {

            }

            return Content("");
        }
    }
}