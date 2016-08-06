using System.Web.Mvc;

namespace EscarGoQueue
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute
            {
                View = "Error"
            }, 1);
        }
    }
}
