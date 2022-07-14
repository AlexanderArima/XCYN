using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace XCYN.Web.Pages.SSE
{
    /// <summary>
    /// SSEHandler 的摘要说明
    /// </summary>
    public class SSEHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.ContentType = "text/event-stream; charset=utf-8";  // 设置报文头为text/event-stream
                context.Response.Headers["Cache-control"] = "no-cache";  // 规定不对页面进行缓存
                context.Response.Headers["Keep-alive"] = "timeout=5";
                context.Response.Expires = -1;
                context.Response.StatusCode = 200;
                for (int i = 0; ;)
                {
                    try
                    {
                        context.Response.Write(string.Format("id:11\nevent:message\ndata:{0}\n\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        context.Response.Flush();
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
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}