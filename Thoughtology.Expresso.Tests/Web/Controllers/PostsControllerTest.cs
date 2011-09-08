using System;
using System.Web.Mvc;
using Xunit;
using Xunit.Extensions;
using Thoughtology.Expresso.Controllers;

namespace Thoughtology.Expresso.Tests.Web.Controllers
{
    public class PostsControllerTest
    {
        [Fact]
        public void Constructor_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new PostsController(null));
        }

        [Theory]
        [InlineData("")]
        public void Index_DoesNotReturnNull(object postService)
        {
            var sut = new PostsController(postService);

            var result = sut.Index();

            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("")]
        public void Index_ReturnsEmptyResult(object postService)
        {
            var sut = new PostsController(postService);

            var result = sut.Index();

            Assert.IsType<EmptyResult>(result);
        }
    }
}