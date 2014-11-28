using Autofac;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Services.Configuration
{
    /// <summary>
    /// Registers components to the Inversion of Control container that are related to the business services.
    /// </summary>
    public class ServiceModule : Module
    {
        /// <summary>
        /// Adds registrations to the container.
        /// </summary>
        /// <param name="builder">
        /// The builder through which components can be registered.
        /// </param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PostQueryService>()
                .As(typeof(IQueryService<Post>));
            builder
                .RegisterType<TagQueryService>()
                .As(typeof(IQueryService<Tag>));
            builder
                .RegisterGeneric(typeof(CommandService<>))
                .As(typeof(ICommandService<>));
        }
    }
}
