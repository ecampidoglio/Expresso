using System.Web.Mvc;
using System.Web.Routing;

namespace Thoughtology.Expresso.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Action",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
