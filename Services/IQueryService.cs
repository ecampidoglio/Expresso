using System.Collections.Generic;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Defines the query operations that can be performed to retrieve <typeparamref name="TEntity"/> entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to retrieve.</typeparam>
    public interface IQueryService<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Retrieves all <typeparamref name="TEntity"/> entities.
        /// </summary>
        /// <returns>
        /// A sequence of <typeparamref name="TEntity"/> objects or an empty sequence when none was found.
        /// </returns>
        IEnumerable<TEntity> Find();
    }

    /// <summary>
    /// Defines the query operations that can be performed to retrieve loosely typed entities.
    /// </summary>
    public interface IQueryService : IQueryService<object>
    {
    }
}
