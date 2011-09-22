using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Thoughtology.Expresso.Controllers;
using Thoughtology.Expresso.Model;
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
            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => new PostsController(null));
        }

        [Theory]
        [AutoMoqData]
        public void Index_DoesNotReturnNull(Mock<IPostQueryService> postQueryService)
        {
            // Given
            var sut = new PostsController(postQueryService.Object);

            // When
            var result = sut.Index();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Index_ReturnsView(Mock<IPostQueryService> postQueryService)
        {
            // Given
            var sut = new PostsController(postQueryService.Object);

            // When
            var result = sut.Index();

            // Then
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [AutoMoqData]
        public void Index_ReturnsViewWithPostsSequenceInViewBag(Mock<IPostQueryService> postQueryService)
        {
            // Given
            var sut = new PostsController(postQueryService.Object);

            // When
            var result = sut.Index() as ViewResult;

            // Then
            Assert.IsAssignableFrom<IEnumerable<Post>>(result.ViewBag.Posts);
        }

        [Theory]
        [AutoMoqData]
        public void Index_WithNoPosts_ReturnsViewWithEmptyPostsSequenceInViewBag(Mock<IPostQueryService> postQueryService)
        {
            // Given
            var sut = new PostsController(postQueryService.Object);

            // When
            var result = sut.Index() as ViewResult;
            var posts = result.ViewBag.Posts as IEnumerable<Post>;

            // Then
            Assert.False(posts.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Index_WithSomePosts_ReturnsViewWithSameNumberOfItemsInPostsSequenceInViewBag(Mock<IPostQueryService> postQueryService, Post[] posts)
        {
            // Given
            postQueryService.Setup(s => s.Find()).Returns(posts);
            var sut = new PostsController(postQueryService.Object);

            // When
            var view = sut.Index() as ViewResult;
            var result = view.ViewBag.Posts as IEnumerable<Post>;

            // Then
            Assert.False(result.Any());
        }
    }
}