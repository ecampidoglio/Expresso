using System;
using System.Web.Mvc;
using Moq;
using Thoughtology.Expresso.Controllers;
using Thoughtology.Expresso.Services;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Web.Controllers
{
    public class PostsControllerTest
    {
        [Fact]
        public void Constructor_WithNullPostQueryService_ThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new PostsController(null));
        }

        [Theory]
        [AutoMoqData]
        public void Index_DoesNotReturnNull(Mock<IPostQueryService> postQueryService)
        {
            var sut = new PostsController(postQueryService.Object);

            var result = sut.Index();

            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Index_ReturnsEmptyResult(Mock<IPostQueryService> postQueryService)
        {
            var sut = new PostsController(postQueryService.Object);

            var result = sut.Index();

            Assert.IsType<EmptyResult>(result);
        }
    }
}