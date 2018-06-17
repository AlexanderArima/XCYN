using System;
using System.IO.Compression;
using System.Web;

namespace XCYN.Web.Module
{
    public class FileGZipModule : IHttpModule
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此模块
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参阅以下链接: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //此处放置清除代码。
        }

        public void Init(HttpApplication context)
        {
            // 下面是如何处理 LogRequest 事件并为其 
            // 提供自定义日志记录实现的示例
            context.BeginRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;

            // 这里做个简单的演示，只处理aspx页面的输出压缩。
            // 当然了，IIS也提供压缩功能，这里也仅当演示用，或许可适用于一些特殊场合。
            if (app.Request.AppRelativeCurrentExecutionFilePath.EndsWith(
                                "aspx", StringComparison.OrdinalIgnoreCase) == false)
                // 注意：先判断是不是要处理的请求，如果不是，直接退出。
                //        而不是：先执行了后面的判断，再发现不是aspx时才退出。
                return;


            string flag = app.Request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(flag) == false && flag.ToLower().IndexOf("gzip") >= 0)
            {
                app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "gzip");
            }
        }
    }
}
