using System.Data.Entity;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Represents the entry point for the Entity Framework API.
    /// </summary>
    public class DataContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Gets or sets a collection of <see cref="Posts"/> entities.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Performs any pending operations on the data store as a single unit.
        /// </summary>
        public void Commit()
        {
            this.SaveChanges();
        }
    }
}