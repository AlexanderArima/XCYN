using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using XCYN.Common;

namespace XCYN.Web.Pages.test
{
    /// <summary>
    /// RequestHandler 的摘要说明
    /// </summary>
    public class RequestHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var s1 = RequestHelper.GetServerString(RequestHelper.ServerVar.ALL_HTTP);
            var action = context.Request["action"];
            switch (action)
            {
                case "IsGetTest":
                    IsGetTest();
                    break;
                case "IsPostTest":
                    IsPostTest();
                    break;
                case "GetJS":
                    GetJS(context);
                    break;
                case "ClearCache":
                    ClearCache(context);
                    break;
                default:
                    break;
            }
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="context"></param>
        private void ClearCache(HttpContext context)
        {
            var id = context.Request["id"];
            if(id == "1")
            {
                //清除key1的缓存
                HttpRuntime.Cache.Remove("key1");
            }
            else if(id == "2")
            {
                //清除key2的缓存
                HttpRuntime.Cache.Remove("key2");
            }
            //分别返回key1和key2的值
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict["key1"] = HttpRuntime.Cache.Get("key1");
            dict["key2"] = HttpRuntime.Cache.Get("key2");
            var json = JsonConvert.SerializeObject(dict);
            context.Response.Clear();
            context.Response.Write(json);
            context.Response.End();
        }

        public void GetJS(HttpContext context)
        {
            Thread.Sleep(5000);
            context.Response.ContentType = "application/json";
            context.Response.Write(@"function Hello()
            {
                alert(""hello world"");
            }");
        }

        public void IsGetTest()
        {
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
        }

        public void IsPostTest()
        {
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
        }

        public void GetPhoto(HttpContext context)
        {
            //var index = Convert.ToInt32(context.Request["index"]);
            ////从数据库中取出数据
            //var query = from a in db.photo
            //            select a;
            //var data = query.Skip(10 * index).take(10);//取下标为30-40的数据
            //var json = JsonConvert.SerializeObject(data);//将对象序列化为json
            //context.Response.Write(json);//返回给客户端浏览器
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