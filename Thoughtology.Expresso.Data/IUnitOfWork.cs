using System;
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
        IDbSet<TEntity> Get<TEntity>() where TEntity : class;

        /// <summary>
        /// Performs any pending operations on the data store as a single unit.
        /// </summary>
        void Commit();
    }
}