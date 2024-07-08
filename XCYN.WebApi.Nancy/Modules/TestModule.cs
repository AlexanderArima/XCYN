using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.WebApi.Nancy.Modules
{
    public class TestModule : NancyModule
    {
        // public TestModule() : base("/order")
        public TestModule()
        {
            Get["/"] = _ =>
            {
                return "Hello World";
            };

            // 登录接口.
            Get["login"] = _ =>
            {
                Login_RequestObject req = this.Bind();
                if (req == null)
                {
                    return HttpStatusCode.BadRequest;
                }

                if (req.username == "admin" && req.password == "123456")
                {
                    BasicUser.user = new BasicUser() { UserName = "admin" };
                    return "登录成功";
                }
                else
                {
                    return "用户名或密码错误";
                }
            };

            Get["/list"] = _ =>
            {
                return "Hello World List";
            };

            Get["/list/{pageSize:int}"] = _ =>
            {
                return "Hello World List pageSize=" + _.pageSize;
            };

            Get["/get", p => {
                var query = p.Request.Query as DynamicDictionary;
                var param1 = query.Values.ElementAt(0);
                if (param1 == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }] = _ =>
            {
                return "Hello World Get";
            };

            Before += ctx =>
            {
                // Before拦截器，可以让你拦截请求甚至取消它
                var c = Context;
                var header = c.Request.Headers.ToList();
                if(c.Request.Path == "/" || c.Request.Path == "/login")
                {
                    return null;
                }

                // 鉴权
                if (BasicUser.user == null)
                {
                    return HttpStatusCode.Unauthorized;
                }
                else
                {
                    return ctx.Response ;
                }
            };

            After += ctx =>
            {
                // After拦截器也是一样，只是没有返回值
            };

            OnError += (ctx, ex) =>
            {
                // OnError系统出现异常时在这里捕获
                return Response.AsText(ex.Message).WithStatusCode(HttpStatusCode.OK);
            };
        }

        public class Login_RequestObject
        {
            /// <summary>
            /// 用户名.
            /// </summary>
            public string username { get; set; }

            /// <summary>
            /// 密码.
            /// </summary>
            public string password { get; set; }
        }
    }
}