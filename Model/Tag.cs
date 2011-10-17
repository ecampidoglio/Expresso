using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thoughtology.Expresso.Model
{
    /// <summary>
    /// Reprents a tag used to categorize and group related <see cref="Post"/> items.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        public Tag()
        {
            this.Posts = new List<Post>();
        }

        /// <summary>
        /// Gets or sets the unique numeric identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of posts associated with this tag.
        /// </summary>
        public ICollection<Post> Posts { get; private set; }
    }
}
