using System;
using System.Linq;
using Moq;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Services;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Services
{
    public class PostQueryServiceTest
    {
        [Fact]
        public void Constructor_WithNullPostRepository_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => new PostQueryService(null));
        }

        [Theory]
        [AutoMoqData]
        public void Find_DoesNotReturnNull(Mock<IRepository<Post>> postRepository)
        {
            // Given
            var sut = new PostQueryService(postRepository.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithNoPosts_ReturnsEmptySequence(Mock<IRepository<Post>> postRepository)
        {
            // Given
            var sut = new PostQueryService(postRepository.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.False(result.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithSomePosts_ReturnsSameNumberOfPosts(Mock<IRepository<Post>> postRepository, Post[] posts)
        {
            // Given
            postRepository.Setup(s => s.FindAll()).Returns(posts);
            var sut = new PostQueryService(postRepository.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.Equal(posts.Count(), result.Count());
        }
    }
}