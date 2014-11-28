using System;
using System.Collections.Generic;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Implements the query operations that can be performed to retrieve
    /// <see cref="Post"/> entities.
    /// </summary>
    public class PostQueryService : IQueryService<Post>
    {
        private readonly IRepository<Post> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostQueryService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="repository"/> is null.
        /// </exception>
        public PostQueryService(IRepository<Post> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        /// <summary>
        /// Retrieves all <see cref="Post"/> entities.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="Post"/> objects
        /// or an empty sequence when none was found.
        /// </returns>
        public IEnumerable<Post> Find()
        {
            return this.repository.Find("Tags");
        }
    }
}
