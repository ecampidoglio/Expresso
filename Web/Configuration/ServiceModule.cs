using Autofac;
using Autofac.Integration.Mvc;
using Thoughtology.Expresso.Services;

namespace Thoughtology.Expresso.Web.Configuration
{
    /// <summary>
    /// Registers components to the Inversion of Control container that are related to the business services.
    /// </summary>
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(QueryService<>)).InstancePerHttpRequest().As(typeof(IQueryService<>));
            builder.RegisterGeneric(typeof(CommandService<>)).InstancePerHttpRequest().As(typeof(ICommandService<>));
        }
    }
}