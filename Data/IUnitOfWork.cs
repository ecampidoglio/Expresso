using System;
using System.Collections.Generic;

namespace Thoughtology.Expresso.Data
{
    /// <summary>
    /// Defines the role interface used to perform groups of operations against a data store as a single unit.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Retrieves the collection of entities of the specified <typeparamref name="TEntity"/> type from the data store.
        /// </summary>
        /// <typeparam name="TEntity">The type of entities to retrieve.</typeparam>
        /// <param name="includedPropertyPaths">
        /// The list of properties on the specified <typeparamref name="TEntity"/> type to include in the results.
        /// It is possible to specify properties on related objects using the <strong>Dot notation</strong>.
        /// </param>
        /// <returns>The set of entities.</returns>
        IEnumerable<TEntity> Get<TEntity>(params string[] includedPropertyPaths)
            where TEntity : class;

        /// <summary>
        /// Schedules the specified <typeparamref name="TEntity"/> object to be added to the data store.
        /// The operation will first take effect when the <see cref="M:Commit"/> method is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to add.</typeparam>
        /// <param name="entity">The entity object to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        void RegisterAdded<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Schedules the specified <typeparamref name="TEntity"/> object to be updated in the data store.
        /// The operation will first take effect when the <see cref="M:Commit"/> method is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to update.</typeparam>
        /// <param name="entity">The entity object to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        void RegisterModified<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Schedules the specified <typeparamref name="TEntity"/> object to be deleted from the data store.
        /// The operation will first take effect when the <see cref="M:Commit"/> method is called.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to delete.</typeparam>
        /// <param name="entity">The entity object to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Performs any pending operations on the data store as a single unit.
        /// </summary>
        void Commit();
    }
}
