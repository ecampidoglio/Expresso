using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thoughtology.Expresso.Model
{
    /// <summary>
    /// Represents a blog post that is either published or in draft. 
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post()
        {
            this.ModifiedTimestamp = DateTime.Now;
            this.Tags = new List<Tag>();
        }

        /// <summary>
        /// Gets or sets the unique numeric identifier.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Markdown-formatted blog post content. 
        /// </summary>
        public string MarkdownContent { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the post was last modified.
        /// </summary>
        public DateTime ModifiedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the post was published.
        /// </summary>
        /// <remarks>
        /// If this property is <see langref="null"/> the post is currently a draft
        /// and is not visible on the web site.
        /// </remarks>
        public DateTime? PublishedTimestamp { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the post is draft.
        /// </summary>
        public bool IsDraft { get; set; }

        /// <summary>
        /// Gets the collection of tags associated to this post.
        /// </summary>
        public ICollection<Tag> Tags { get; private set; }
    }
}
