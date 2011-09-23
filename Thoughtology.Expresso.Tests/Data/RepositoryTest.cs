using System;
using System.Linq;
using Moq;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Data
{
    public class RepositoryTest
    {
        [Fact]
        public void Constructor_WithNullUnitOfWork_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => new Repository<object>(null));
        }

        [Theory]
        [AutoMoqData]
        public void Constructor_SutIsIRepository(Mock<IUnitOfWork> unitOfWork)
        {
            // When
            var sut = new Repository<object>(unitOfWork.Object);

            // Then
            Assert.IsAssignableFrom<IRepository<object>>(sut);
        }

        [Theory]
        [AutoMoqData]
        public void FindAll_DoesNotReturnNull(Mock<IUnitOfWork> unitOfWork, DbSetStub<object> dbSet)
        {
            // Given
            unitOfWork.Setup(s => s.Get<object>()).Returns(dbSet);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            var result = sut.FindAll();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void FindAll_WithNoResults_ReturnsEmptySequence(Mock<IUnitOfWork> unitOfWork, DbSetStub<object> dbSet)
        {
            // Given
            unitOfWork.Setup(s => s.Get<object>()).Returns(dbSet);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            var result = sut.FindAll();

            // Then
            Assert.False(result.Any());
        }

        [Theory]
        [AutoMoqData]
        public void FindAll_WithSomeItems_ReturnsSameNumberOfItems(
            Mock<IUnitOfWork> unitOfWork,
            DbSetStub<object> dbSet,
            object[] entities)
        {
            // Given
            dbSet.Entities = entities;
            unitOfWork.Setup(s => s.Get<object>()).Returns(dbSet);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            var result = sut.FindAll();

            // Then
            Assert.Equal(dbSet.Count(), result.Count());
        }
    }
}