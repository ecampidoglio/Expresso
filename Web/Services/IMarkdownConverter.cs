namespace Thoughtology.Expresso.Web.Services
{
    /// <summary>
    /// Defines the role of a Markdown to HTML text converter.
    /// </summary>
    /// <remarks>
    /// Markdown is a text-to-HTML conversion tool for web writers.
    /// Markdown allows you to write using an easy-to-read, easy-to-write plain text format,
    /// then convert it to structurally valid XHTML (or HTML).
    /// </remarks>
    public interface IMarkdownConverter
    {
        /// <summary>
        /// Transforms the provided Markdown-formatted text to HTML.
        /// </summary>
        /// <param name="source">The Markdown-formatted source to convert.</param>
        /// <returns>The converted source in HTML format.</returns>
        string ConvertToHtml(string source);
    }
}
