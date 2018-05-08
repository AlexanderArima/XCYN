using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XCYN.Web.Pages.test
{
    public partial class CacheTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.Cache["name"] == null)
            {
                //Page.Cache访问了HttpContext.Cache
                Page.Cache["name"] = "Wang";
            }
            if(HttpContext.Current.Cache["age"] == null)
            {
                //HttpContext.Cache又直接访问HttpRuntime.Cache
                HttpContext.Current.Cache["age"] = 18;
            }
            if(HttpRuntime.Cache.Get("user") == null)
            {
                //滑动过期，设置为最近一次20分钟不再访问该缓存就释放的策略
                HttpRuntime.Cache.Insert("user", 1, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
                
                //绝对过期，设置为20分钟后缓存就释放的策略
                HttpRuntime.Cache.Insert("user2", 1, null, System.DateTime.UtcNow.AddMinutes(20), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }
    }
}