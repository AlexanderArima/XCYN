using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.Web.Controllers
{
    [MyHanderError]
    public class HomeController : Controller
    {
        public ActionResult Index(int? id)
        {
            throw new Exception();
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        //以下两个是ExecuteResult的拦截器方法
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        //以下两个类是实现拦截的方法
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var param = filterContext.ActionDescriptor.GetParameters();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }

    public class MyHanderErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
            //写入错误日志
            base.OnException(filterContext);
        }
    }
    
    //ExecuteResult的拦截器方法
    public class MyCustomResultAttribute: ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }

    //定义一个原子类，只要实现了这个类就能实现拦截
    public class MyCustomFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var param = filterContext.ActionDescriptor.GetParameters();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //filterContext.Result = new ViewResult()
            //{
            //    ViewName = "Error"
            //};
        }
    }

    public class StaticFileWriteFilterAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Filter = new StaticFileWriteResponseFilterWrapper(filterContext.HttpContext.Response.Filter, filterContext);
        }

        class StaticFileWriteResponseFilterWrapper : System.IO.Stream
        {
            private System.IO.Stream inner;
            private ControllerContext context;
            public StaticFileWriteResponseFilterWrapper(System.IO.Stream s, ControllerContext context)
            {
                this.inner = s;
                this.context = context;
            }

            public override bool CanRead
            {
                get { return inner.CanRead; }
            }

            public override bool CanSeek
            {
                get { return inner.CanSeek; }
            }

            public override bool CanWrite
            {
                get { return inner.CanWrite; }
            }

            public override void Flush()
            {
                inner.Flush();
            }

            public override long Length
            {
                get { return inner.Length; }
            }

            public override long Position
            {
                get
                {
                    return inner.Position;
                }
                set
                {
                    inner.Position = value;
                }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return inner.Read(buffer, offset, count);
            }

            public override long Seek(long offset, System.IO.SeekOrigin origin)
            {
                return inner.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                inner.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                inner.Write(buffer, offset, count);
                try
                {
                    string p = context.HttpContext.Server.MapPath(HttpContext.Current.Request.Path);

                    if (Path.HasExtension(p))
                    {
                        string dir = Path.GetDirectoryName(p);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        if (File.Exists(p))
                        {
                            File.Delete(p);
                        }
                        File.AppendAllText(p, System.Text.Encoding.UTF8.GetString(buffer));
                    }
                }
                catch
                {

                }
            }
        }
    }
}