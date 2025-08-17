using SINPE.Empresarial.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace SINPE.Empresarial.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionFilter());
        }
    }
}
