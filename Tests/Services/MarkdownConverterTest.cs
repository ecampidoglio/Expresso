using System;
using Ploeh.AutoFixture.Xunit;
using Thoughtology.Expresso.Services;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Services
{
    public class MarkdownConverterTest
    {
        [Fact]
        public void ConvertToHtml_WithNullSource_ReturnsEmptyString()
        {
            var sut = new MarkdownConverter();

            var result = sut.ConvertToHtml(null);

            Assert.Equal(String.Empty, result);
        }

        [Fact]
        public void ConvertToHtml_WithEmptyStringSource_ReturnsEmptyString()
        {
            var sut = new MarkdownConverter();

            var result = sut.ConvertToHtml(String.Empty);

            Assert.Equal(String.Empty, result);
        }

        [Theory]
        [AutoData]
        public void ConvertToHtml_WithMarkdownText_ReturnsHtmlPText([Frozen] string text)
        {
            var source = text;
            var expectedResult = String.Format("<p>{0}</p>\n", text);
            var sut = new MarkdownConverter();

            var result = sut.ConvertToHtml(source);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [AutoData]
        public void ConvertToHtml_WithMarkdownH1Text_ReturnsHtmlH1Text([Frozen] string text)
        {
            var source = String.Format("# {0}", text);
            var expectedResult = String.Format("<h1>{0}</h1>\n", text);
            var sut = new MarkdownConverter();

            var result = sut.ConvertToHtml(source);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [AutoData]
        public void ConvertToHtml_WithMarkdownH2Text_ReturnsHtmlH2Text([Frozen] string text)
        {
            var source = String.Format("## {0}", text);
            var expectedResult = String.Format("<h2>{0}</h2>\n", text);
            var sut = new MarkdownConverter();

            var result = sut.ConvertToHtml(source);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [AutoData]
        public void ConvertToHtml_WithMarkdownBoldText_ReturnsHtmlBoldText([Frozen] string text)
        {
            var source = String.Format("**{0}**", text);
            var expectedResult = String.Format("<p><strong>{0}</strong></p>\n", text);
            var sut = new MarkdownConverter();

            var result = sut.ConvertToHtml(source);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [AutoData]
        public void ConvertToHtml_WithMarkdownItalicText_ReturnsHtmlItalicText([Frozen] string text)
        {
            var source = String.Format("*{0}*", text);
            var expectedResult = String.Format("<p><em>{0}</em></p>\n", text);
            var sut = new MarkdownConverter();

            var result = sut.ConvertToHtml(source);

            Assert.Equal(expectedResult, result);
        }
    }
}
