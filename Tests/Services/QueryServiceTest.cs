using System;
using System.Linq;
using Moq;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Services;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Services
{
    public class QueryServiceTest
    {
        [Theory]
        [AutoMoqData]
        public void Constructor_SutIsQueryService(Mock<IRepository> repository)
        {
            // When
            var sut = new QueryService<object>(repository.Object);

            // Then
            Assert.IsAssignableFrom<IQueryService<object>>(sut);
        }

        [Fact]
        public void Constructor_WithNullRepository_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => new QueryService<object>(null));
        }

        [Theory]
        [AutoMoqData]
        public void Find_DoesNotReturnNull(Mock<IRepository> repository)
        {
            // Given
            var sut = new QueryService<object>(repository.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithNoItems_ReturnsEmptySequence(Mock<IRepository> repository)
        {
            // Given
            var sut = new QueryService<object>(repository.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.False(result.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithSomeItems_ReturnsSameNumberOfResults(Mock<IRepository> repository, object[] items)
        {
            // Given
            repository.Setup(s => s.FindAll()).Returns(items);
            var sut = new QueryService<object>(repository.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.Equal(items.Count(), result.Count());
        }
    }
}
