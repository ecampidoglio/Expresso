using System;
using System.Collections.Generic;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Implements the query operations that can be performed to retrieve
    /// <see cref="Tag"/> entities.
    /// </summary>
    public class TagQueryService : IQueryService<Tag>
    {
        private readonly IRepository<Tag> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagQueryService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="repository"/> is null.
        /// </exception>
        public TagQueryService(IRepository<Tag> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        /// <summary>
        /// Retrieves all <see cref="Tag"/> entities.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="Tag"/> objects
        /// or an empty sequence when none was found.
        /// </returns>
        public IEnumerable<Tag> Find()
        {
            return this.repository.Find("Posts");
        }
    }
}