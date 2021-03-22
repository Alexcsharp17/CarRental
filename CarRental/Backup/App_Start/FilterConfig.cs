using System.Web;
using System.Web.Mvc;
using CarRental.BLL.Attributes;

namespace CarRental.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionLoggerAttribute());
            filters.Add(new LogFilterAttribute());
        }
    }
}
