﻿using System;
using System.Data;
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
        /// Gets the collection of <see cref="Posts"/> entities in the data store.
        /// </summary>
        public IDbSet<Post> Posts
        {
            get { return Get<Post>(); }
        }

        /// <summary>
        /// Retrieves the collection of entities of the specified <typeparamref name="TEntity"/> type from the data store.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities to retrieve.</typeparam>
        /// <returns>The set of entities.</returns>
        public IDbSet<TEntity> Get<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// Retrieves the <see cref="EntityState"/> of the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to retrieve the status for.</typeparam>
        /// <param name="entity">The entity instance to retrieve the status for.</param>
        /// <returns>
        /// A member of the <see cref="EntityState"/> enumeration.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public EntityState GetState<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return Entry(entity).State;
        }

        /// <summary>
        /// Performs any pending operations on the data store as a single unit.
        /// </summary>
        public void Commit()
        {
            SaveChanges();
        }
    }
}
