using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCYN.MVC.Models;

namespace XCYN.MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 验证用户是否有效.
        /// </summary>
        /// <returns></returns>
        public string ValidUser(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ReceiveObject obj = new ReceiveObject();
                obj.code = "999999";
                obj.msg = "用户名、密码不能为空";
                return JsonConvert.SerializeObject(obj);
            }

            if(this.Valid(username, password))
            {
                ReceiveObject obj = new ReceiveObject();
                obj.code = "0";
                obj.msg = "成功";
                return JsonConvert.SerializeObject(obj);
            }
            else
            {
                ReceiveObject obj = new ReceiveObject();
                obj.code = "999999";
                obj.msg = "用户名、密码错误";
                return JsonConvert.SerializeObject(obj);
            }
        }

        private bool Valid(string username, string password)
        {
            if(username.Equals("admin") && password.Equals("123456"))
            {
                return true;
            }

            return false;
        }
    }
}