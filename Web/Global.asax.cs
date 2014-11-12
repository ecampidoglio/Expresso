using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Thoughtology.Expresso.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            DependencyConfig.RegisterContainer();
        }
    }
}
