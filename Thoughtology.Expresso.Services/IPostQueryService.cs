using System.Collections.Generic;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Defines the query operations that can be performed to retrieve <see cref="Post"/> entities.
    /// </summary>
    public interface IPostQueryService
    {
        /// <summary>
        /// Retrieves all <see cref="Post"/> entities.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="Post"/> objects or an empty sequence when none was found.
        /// </returns>
        IEnumerable<Post> Find();
    }
}