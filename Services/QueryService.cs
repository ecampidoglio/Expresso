using System;
using System.Collections.Generic;
using Thoughtology.Expresso.Data;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Implements the query operations that can be performed to retrieve <typeparamref name="TEntity"/> entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to retrieve.</typeparam>
    public class QueryService<TEntity> : IQueryService<TEntity>
        where TEntity : class
    {
        private readonly IRepository<TEntity> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostQueryService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <exception cref="ArgumentNullException"><paramref name="repository"/> is null.</exception>
        public QueryService(IRepository<TEntity> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        /// <summary>
        /// Retrieves all <typeparamref name="TEntity"/> entities.
        /// </summary>
        /// <returns>
        /// A sequence of <typeparamref name="TEntity"/> objects or an empty sequence when none was found.
        /// </returns>
        public IEnumerable<TEntity> Find()
        {
            return repository.FindAll();
        }
    }
}