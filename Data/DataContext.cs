using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Represents the entry point for the Entity Framework API.
    /// </summary>
    public class DataContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Initializes static members of the <see cref="DataContext"/> class.
        /// </summary>
        static DataContext()
        {
            DatabaseInitializer.Configure();
        }

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
        /// Gets the collection of <see cref="Post"/> entities in the data store.
        /// </summary>
        public IDbSet<Post> Posts
        {
            get { return Set<Post>(); }
        }

        /// <summary>
        /// Gets the collection of <see cref="Tag"/> entities in the data store.
        /// </summary>
        public IDbSet<Tag> Tags
        {
            get { return Set<Tag>(); }
        }

        /// <summary>
        /// Retrieves the collection of entities of the specified <typeparamref name="TEntity"/> type from the data store.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities to retrieve.</typeparam>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified <typeparamref name="TEntity"/> type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>The set of entities.</returns>
        public IEnumerable<TEntity> Get<TEntity>(params string[] includedPropertyPaths) where TEntity : class
        {
            DbQuery<TEntity> query = CreateBaseQuery<TEntity>();

            foreach (var path in includedPropertyPaths)
            {
                query = query.Include(path);
            }

            return query;
        }

        /// <summary>
        /// Schedules the specified <typeparamref name="TEntity"/> object to be added to the data store.
        /// The operation will first take effect when the <see cref="M:Commit"/> method is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to add.</typeparam>
        /// <param name="entity">The entity object to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public void RegisterAdded<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Schedules the specified <typeparamref name="TEntity"/> object to be updated in the data store.
        /// The operation will first take effect when the <see cref="M:Commit"/> method is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to update.</typeparam>
        /// <param name="entity">The entity object to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public void RegisterModified<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Set<TEntity>().Attach(entity);
            Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Schedules the specified <typeparamref name="TEntity"/> object to be deleted from the data store.
        /// The operation will first take effect when the <see cref="M:Commit"/> method is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to delete.</typeparam>
        /// <param name="entity">The entity object to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Performs any pending operations on the data store as a single unit.
        /// </summary>
        public void Commit()
        {
            SaveChanges();
        }

        private DbSet<TEntity> CreateBaseQuery<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }
    }
}
