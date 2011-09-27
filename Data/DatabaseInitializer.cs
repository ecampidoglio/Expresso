using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Thoughtology.Expresso.Data;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DatabaseInitializer), "Configure")]
namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Initializes the target database properties as required by <c>Entity Framework 4.1</c>.
    /// </summary>
    public static class DatabaseInitializer
    {
        /// <summary>
        /// Configures the target database properties.
        /// </summary>
        public static void Configure()
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }
    }
}