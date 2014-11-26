using Autofac;

namespace Thoughtology.Expresso.Data.Configuration
{
    /// <summary>
    /// Registers components to the Inversion of Control container that are related to data access.
    /// </summary>
    public class RepositoryModule : Module
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryModule" /> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string used to connect to the database.
        /// </param>
        public RepositoryModule(string connectionString)
        {
            ConnectionString = connectionString;
        }

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
            builder
                .Register(c => new DataContext(this.ConnectionString))
                .As<IUnitOfWork>();
            builder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));
        }
    }
}
