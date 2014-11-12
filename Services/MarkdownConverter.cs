using System;
using MarkdownSharp;

namespace Thoughtology.Expresso.Services
{
    /// <summary>
    /// Implements the role of a Markdown to HTML text converter.
    /// </summary>
    /// <remarks>
    /// Markdown is a text-to-HTML conversion tool for web writers.
    /// Markdown allows you to write using an easy-to-read, easy-to-write plain text format,
    /// then convert it to structurally valid XHTML (or HTML).
    /// </remarks>
    public class MarkdownConverter : IMarkdownConverter
    {
        private readonly Markdown converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownConverter"/> class.
        /// </summary>
        public MarkdownConverter()
        {
            this.converter = new Markdown();
        }

        /// <summary>
        /// Transforms the provided Markdown-formatted text to HTML.
        /// </summary>
        /// <param name="source">The Markdown-formatted source to convert.</param>
        /// <returns>The converted source in HTML format.</returns>
        public string ConvertToHtml(string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return String.Empty;
            }

            return this.converter.Transform(source);
        }
    }
}
