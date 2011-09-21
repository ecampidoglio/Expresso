using System;
using System.Collections.Generic;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Implements the query operations that can be performed to retrieve <see cref="Post"/> entities.
    /// </summary>
    public class PostQueryService : IPostQueryService
    {
        private readonly IRepository<Post> postRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostQueryService"/> class.
        /// </summary>
        /// <param name="postRepository">The repository for <see cref="Post"/> entities.</param>
        public PostQueryService(IRepository<Post> postRepository)
        {
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }

            this.postRepository = postRepository;
        }

        /// <summary>
        /// Retrieves all <see cref="Post"/> entities.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="Post"/> objects or an empty sequence when none was found.
        /// </returns>
        public IEnumerable<Post> Find()
        {
            return postRepository.FindAll();
        }
    }
}