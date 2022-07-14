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
                for (int i = 0; ;i ++)
                {
                    try
                    {
                        LastEventID = i;    // 设置Last-Event-Id的意义在于保证数据的完整性，因为SSE有自动断点重连的的机制，重连成功会将这个属性回传到服务器，服务器接收到这个后就可以做某些处理。
                        HttpContext.Response.Write(string.Format("id:{1}\nevent:message\ndata:{0}\n\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LastEventID));
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