using System.Web;
using System.Web.Mvc;

namespace _016_AngularJs_Mvc_Demo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}