using System.Web;
using System.Web.Mvc;
using XCYN.WebApi.App_Start;

namespace XCYN.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new JsonCallbackAttribute());
        }
    }
}
