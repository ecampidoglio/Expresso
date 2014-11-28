using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Ploeh.AutoFixture.Xunit;
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
        [Theory, AutoMoqData]
        public void Constructor_SutIsPostQueryService(PostQueryService sut)
        {
            // Then
            Assert.IsAssignableFrom<IQueryService<Post>>(sut);
        }

        [Fact]
        public void Constructor_WithNullRepository_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException),
                () => new PostQueryService(null));
        }

        [Theory, AutoMoqData]
        public void Find_DoesNotReturnNull(PostQueryService sut)
        {
            // When
            var result = sut.Find();

            // Then
            Assert.NotNull(result);
        }

        [Theory, AutoMoqData]
        public void Find_WithNoItems_ReturnsEmptySequence(
            [Frozen]Mock<IRepository<Post>> repository,
            PostQueryService sut)
        {
            // Given
            repository
                .Setup(r => r.Find(It.IsAny<string>()))
                .Returns(new Post[0]);

            // When
            var result = sut.Find();

            // Then
            Assert.False(result.Any());
        }

        [Theory, AutoMoqData]
        public void Find_WithSomeItems_ReturnsSameNumberOfResults(
            [Frozen]Mock<IRepository<Post>> repository,
            IEnumerable<Post> items,
            PostQueryService sut)
        {
            // Given
            repository
                .Setup(s => s.Find(It.IsAny<string[]>()))
                .Returns(items);

            // When
            var result = sut.Find();

            // Then
            Assert.Equal(items.Count(), result.Count());
        }

        [Theory, AutoMoqData]
        public void Find_EagerLoadsAssociatedTags(
            [Frozen]Mock<IRepository<Post>> repository,
            PostQueryService sut)
        {
            // When
            sut.Find();

            // Then
            repository.Verify(m => m.Find("Tags"));
        }
    }
}
