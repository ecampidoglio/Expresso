namespace Thoughtology.Expresso.Model
{
    /// <summary>
    /// Represents a blog post that is either published or in draft. 
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the unique numeric identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Markdown-formatted blog post content. 
        /// </summary>
        public string MarkdownContent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the blog post is published or in draft.
        /// </summary>
        public bool IsPublished { get; set; }
    }
}