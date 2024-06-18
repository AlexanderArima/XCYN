using System.Web;
using System.Web.Mvc;

namespace XCYN.MVC202406
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
