using System.Web;
using System.Web.Mvc;
using XCYN.Web.Controllers;

namespace XCYN.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyCustomFilterAttribute());
            filters.Add(new StaticFileWriteFilterAttribute());
        }
    }
}
