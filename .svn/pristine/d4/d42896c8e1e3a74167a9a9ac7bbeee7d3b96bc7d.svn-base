using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCYN.Knockout.Models;
using Newtonsoft.Json;

namespace XCYN.Knockout.ashx
{
    /// <summary>
    /// ShopHandler 的摘要说明
    /// </summary>
    public class ShopHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request["action"];
            switch (action)
            {
                case "GetProductList":
                    GetProductList(context);
                    break;
                default:
                    break;
            }
        }

        private void GetProductList(HttpContext context)
        {
            using (ShopEntities db = new ShopEntities())
            {
                //加载所有的产品信息
                var query = from a in db.Products
                            where a.state == true
                            select a;
                var list = query.Take(10).ToList();
                var json = JsonConvert.SerializeObject(list);
                context.Response.Write(json);
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