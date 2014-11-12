using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Thoughtology.Expresso.Data.Configuration;
using Thoughtology.Expresso.Services.Configuration;

namespace Thoughtology.Expresso.Web
{
    public static class DependencyConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
