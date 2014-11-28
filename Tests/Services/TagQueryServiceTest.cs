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
    public class TagQueryServiceTest
    {
        [Theory, AutoMoqData]
        public void Constructor_SutIsTagQueryService(TagQueryService sut)
        {
            // Then
            Assert.IsAssignableFrom<IQueryService<Tag>>(sut);
        }

        [Fact]
        public void Constructor_WithNullRepository_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException),
                () => new TagQueryService(null));
        }

        [Theory, AutoMoqData]
        public void Find_DoesNotReturnNull(TagQueryService sut)
        {
            // When
            var result = sut.Find();

            // Then
            Assert.NotNull(result);
        }

        [Theory, AutoMoqData]
        public void Find_WithNoItems_ReturnsEmptySequence(
            [Frozen]Mock<IRepository<Tag>> repository,
            TagQueryService sut)
        {
            // Given
            repository
                .Setup(r => r.Find(It.IsAny<string>()))
                .Returns(new Tag[0]);

            // When
            var result = sut.Find();

            // Then
            Assert.False(result.Any());
        }

        [Theory, AutoMoqData]
        public void Find_WithSomeItems_ReturnsSameNumberOfResults(
            [Frozen]Mock<IRepository<Tag>> repository,
            IEnumerable<Tag> items,
            TagQueryService sut)
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
            [Frozen]Mock<IRepository<Tag>> repository,
            TagQueryService sut)
        {
            // When
            sut.Find();

            // Then
            repository.Verify(m => m.Find("Posts"));
        }
    }
}