using System;
using System.Data;
using System.Data.Entity;

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
        /// <returns>The set of entities.</returns>
        IDbSet<TEntity> Get<TEntity>()
            where TEntity: class;

        /// <summary>
        /// Retrieves the <see cref="EntityState"/> of the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to retrieve the status for.</typeparam>
        /// <param name="entity">The entity instance to retrieve the status for.</param>
        /// <returns>
        /// A member of the <see cref="EntityState"/> enumeration.
        /// </returns>
        EntityState GetState<TEntity>(TEntity entity)
            where TEntity: class;

        /// <summary>
        /// Performs any pending operations on the data store as a single unit.
        /// </summary>
        void Commit();
    }
}
