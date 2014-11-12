using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Services;
using Thoughtology.Expresso.Tests.Foundation;
using Thoughtology.Expresso.Web.Controllers;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Web.Controllers
{
    public class PostControllerTest
    {
        [Theory]
        [AutoMoqData]
        public void Constructor_SutIsController(Mock<IQueryService<Post>> postQueryService)
        {
            // When
            var sut = new PostController(postQueryService.Object);

            // Then
            Assert.IsAssignableFrom<Controller>(sut);
        }

        [Fact]
        public void Constructor_WithNullPostQueryService_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => new PostController(null));
        }

        [Theory]
        [AutoMoqData]
        public void Index_DoesNotReturnNull(Mock<IQueryService<Post>> postQueryService)
        {
            // Given
            var sut = new PostController(postQueryService.Object);

            // When
            var result = sut.Index();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Index_ReturnsView(Mock<IQueryService<Post>> postQueryService)
        {
            // Given
            var sut = new PostController(postQueryService.Object);

            // When
            var result = sut.Index();

            // Then
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [AutoMoqData]
        public void Index_ReturnsViewWithPostsSequenceInViewBag(Mock<IQueryService<Post>> postQueryService)
        {
            // Given
            var sut = new PostController(postQueryService.Object);

            // When
            var result = sut.Index();

            // Then
            Assert.IsAssignableFrom<IEnumerable<Post>>(result.ViewData.Model);
        }

        [Theory]
        [AutoMoqData]
        public void Index_WithNoPosts_ReturnsViewWithEmptyPostsSequenceInViewBag(Mock<IQueryService<Post>> postQueryService)
        {
            // Given
            var sut = new PostController(postQueryService.Object);

            // When
            var result = sut.Index();
            var posts = result.ViewData.Model as IEnumerable<Post>;

            // Then
            Assert.False(posts.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Index_WithSomePosts_ReturnsViewWithSameNumberOfItemsInPostsSequenceInViewBag(
        Mock<IQueryService<Post>> postQueryService,
        Post[] posts)
        {
            // Given
            postQueryService.Setup(s => s.Find()).Returns(posts);
            var sut = new PostController(postQueryService.Object);

            // When
            var view = sut.Index();
            var result = view.ViewData.Model as IEnumerable<Post>;

            // Then
            Assert.Equal(posts.Count(), result.Count());
        }
    }
}
