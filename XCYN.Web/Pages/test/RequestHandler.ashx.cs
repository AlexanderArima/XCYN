using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
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
                case "MoveOrder":
                    MoveOrder(context);
                    break;
                case "AddUser":
                    AddUser(context);
                    break;
                default:
                    break;
            }
            var isGet = RequestHelper.IsGet();

            var isPost = RequestHelper.IsPost();
        }
        

        /// <summary>
        /// 移动栏目的顺序后，一段时间不操作即发送邮件
        /// </summary>
        /// <param name="context"></param>
        private void MoveOrder(HttpContext context)
        {
            var direct = context.Request["direct"];
            HttpRuntime.Cache.Insert("direct", direct, null, 
                System.Web.Caching.Cache.NoAbsoluteExpiration, 
                TimeSpan.FromMinutes(1),CacheItemPriority.NotRemovable, delegate (string key,object value, CacheItemRemovedReason reason) {
                    if (reason == CacheItemRemovedReason.Removed)
                        return;        // 忽略后续调用HttpRuntime.Cache.Insert()所触发的操作

                    // 最后发一次邮件。整个延迟发邮件的过程就处理完了。
                    Debug.WriteLine($"发送邮件,Key:{key},value:{value},reason:{reason}");
                });
           
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="context"></param>
        private void AddUser(HttpContext context)
        { 
            var UserName = context.Request["UserName"];
            var PassWord = context.Request["PassWord"];
            var Sex = context.Request["Sex"];
            var Year = context.Request["Year"];
            var Month = context.Request["Month"];
            var Day = context.Request["Day"];
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("resultCode", 1);

            context.Response.Clear();
            context.Response.ContentType = "text/json";
            context.Response.Write(JsonConvert.SerializeObject(dict));
            context.Response.End();
        }

        private void MoveRecInfoRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            if (reason == CacheItemRemovedReason.Removed)
                return;        // 忽略后续调用HttpRuntime.Cache.Insert()所触发的操作

            // 能运行到这里，就表示是肯定是缓存过期了。
            // 换句话说就是：用户2分钟再也没操作过了。

            // 从参数value取回操作信息
            // MoveRecInfo info = (MoveRecInfo)value;
            // 这里可以对info做其它的处理。

            // 最后发一次邮件。整个延迟发邮件的过程就处理完了。
            // MailSender.SendMail(info);
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