using Autofac;
using Thoughtology.Expresso.Data;

namespace Thoughtology.Expresso.Web.Configuration
{
    /// <summary>
    /// Registers components to the Inversion of Control container that are related to data access.
    /// </summary>
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>().As<IUnitOfWork>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }
    }
}