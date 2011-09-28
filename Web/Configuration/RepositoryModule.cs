using Autofac;
using Autofac.Integration.Mvc;
using Thoughtology.Expresso.Data;

namespace Thoughtology.Expresso.Web.Configuration
{
    /// <summary>
    /// Registers components to the Inversion of Control container that are related to data access.
    /// </summary>
    public class RepositoryModule : Module
    {
        /// <summary>
        /// Gets or sets the connection string used to interact with the data store.
        /// </summary>
        public string ConnectionString { get; set; }

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
            builder.Register(c => new DataContext(ConnectionString)).InstancePerHttpRequest().As<IUnitOfWork>();
            builder.RegisterGeneric(typeof(Repository<>)).InstancePerHttpRequest().As(typeof(IRepository<>));
        }
    }
}