using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Thoughtology.Expresso.Web.Configuration;

namespace Thoughtology.Expresso
{
    /// <summary>
    /// Represents the ASP.NET web application.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Registers the global filters for the application.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// Registers the URL routes for the application.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// Registers the dependency resolver to use in the application.
        /// </summary>
        public static void RegisterDependencyResolver()
        {
            var expressoConnectionString = ConfigurationManager.ConnectionStrings["Expresso"].ConnectionString;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new RepositoryModule { ConnectionString = expressoConnectionString });
            builder.RegisterModule<ServiceModule>();
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        /// <summary>
        /// Handles the <see cref="E:Application.Start"/> event.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            MvcApplication.RegisterGlobalFilters(GlobalFilters.Filters);
            MvcApplication.RegisterRoutes(RouteTable.Routes);
            MvcApplication.RegisterDependencyResolver();
        }
    }
}