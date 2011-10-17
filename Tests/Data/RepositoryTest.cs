using System;
using System.Linq;
using Moq;
using Ploeh.AutoFixture.Xunit;
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
        public void Find_DoesNotReturnNull(Mock<IUnitOfWork> unitOfWork, object[] entities)
        {
            // Given
            unitOfWork.Setup(s => s.Get<object>()).Returns(entities);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithNoResults_ReturnsEmptySequence(Mock<IUnitOfWork> unitOfWork)
        {
            // Given
            unitOfWork.Setup(s => s.Get<object>()).Returns(new object[0]);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.False(result.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithSomeItems_ReturnsSameNumberOfItems(
            Mock<IUnitOfWork> unitOfWork,
            object[] entities)
        {
            // Given
            unitOfWork.Setup(s => s.Get<object>()).Returns(entities);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            var result = sut.Find();

            // Then
            Assert.Equal(entities.Count(), result.Count());
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithEagerLoadedPropertyPaths_InvokesUnitOfWorkWithSamePath(
            Mock<IUnitOfWork> unitOfWork,
            string[] propertyPaths)
        {
            // Given
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            sut.Find(propertyPaths);

            // Then
            unitOfWork.Verify(m => m.Get<object>(propertyPaths));
        }

        [Theory]
        [AutoMoqData]
        public void Find_WithCriteria_ThrowsNotImplementedException(Mock<IUnitOfWork> unitOfWork)
        {
            // Given
            var sut = new Repository<object>(unitOfWork.Object);

            // Then
            Assert.Throws<NotImplementedException>(() => sut.Find(i => true));
        }

        [Theory]
        [AutoMoqData]
        public void Save_WithNullEntity_ThrowsArgumentNullException(Mock<IUnitOfWork> unitOfWork)
        {
            // Given
            var sut = new Repository<object>(unitOfWork.Object);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.Save(null));
        }

        [Theory]
        [AutoMoqData]
        public void Save_WithNewEntity_RegistersEntityAsAdded(
            Mock<IUnitOfWork> unitOfWork,
            object entity,
            object[] entities)
        {
            // Given
            unitOfWork.Setup(s => s.Get<object>()).Returns(entities);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            sut.Save(entity);

            // Then
            unitOfWork.Verify(m => m.RegisterAdded(entity));
        }

        [Theory]
        [AutoMoqData]
        public void Save_WithExistingEntity_RegistersEntityAsModified(
            Mock<IUnitOfWork> unitOfWork,
            [Frozen]object entity,
            object[] entities)
        {
            // Given
            unitOfWork.Setup(s => s.Get<object>()).Returns(entities);
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            sut.Save(entity);

            // Then
            unitOfWork.Verify(m => m.RegisterModified(entity));
        }

        [Theory]
        [AutoMoqData]
        public void Save_WithEntity_CommitsUnitOfWork(
            Mock<IUnitOfWork> unitOfWork,
            object entity)
        {
            // Given
            var sut = new Repository<object>(unitOfWork.Object);

            // When
            sut.Save(entity);

            // Then
            unitOfWork.Verify(m => m.Commit());
        }
    }
}
