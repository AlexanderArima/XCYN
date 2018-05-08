using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCYN.Web.Pages.test
{
    public partial class CacheTest2 : System.Web.UI.Page
    {

        public static string key1 = string.Empty;
        public string key2 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //设置缓存，并设置缓存依赖
            HttpRuntime.Cache.Insert("key1", "cheng");

            //key2依赖于key1，如果key1被删除key2也会被删除
            CacheDependency dep = new CacheDependency(null, new string[] { "key1" });
            HttpRuntime.Cache.Insert("key2", "123456", dep);

            key1 = HttpRuntime.Cache.Get("key1")?.ToString();
            key2 = HttpRuntime.Cache.Get("key2")?.ToString();
        }
    }
}