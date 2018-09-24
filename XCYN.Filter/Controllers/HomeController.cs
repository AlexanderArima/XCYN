using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.Filter.App_Start;

namespace XCYN.Filter.Controllers
{
    public class HomeController : Controller
    {
        // 授权，只有用户名为admin才能进入这个页面
        [Authorize(Users = "admin")]
        public string Index()
        {
            return "这是来自Home控制器的Index方法";
        }

        [CustomException]
        public string Add()
        {
            throw new OverflowException();
        }
        
        [HandleError(ExceptionType = typeof(ArgumentException),View = "ErrorPage")]
        public string Delete()
        {
            throw new ArgumentException("参数名不正确");
        }
    }
}