using System.ComponentModel.DataAnnotations;

namespace Thoughtology.Expresso.Model
{
    /// <summary>
    /// Reprents a tag used to categorize and group related <see cref="Post"/> items.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or sets the unique numeric identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }
    }
}
