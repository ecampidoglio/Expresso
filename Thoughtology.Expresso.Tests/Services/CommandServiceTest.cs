using System;
using Moq;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Services;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Services
{
    public class CommandServiceTest
    {
        [Theory]
        [AutoMoqData]
        public void Constructor_SutIsCommandService(Mock<IUnitOfWork> unitOfWork, Mock<IRepository> repository)
        {
            // When
            var sut = new CommandService<object>(unitOfWork.Object, repository.Object);

            // Then
            Assert.IsAssignableFrom<ICommandService<object>>(sut);
        }

        [Theory]
        [AutoMoqData]
        public void Constructor_WithNullUnitOfWork_ThrowsArgumentNullException(Mock<IRepository> repository)
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => new CommandService<object>(null, repository.Object));
        }

        [Theory]
        [AutoMoqData]
        public void Constructor_WithNullRepository_ThrowsArgumentNullException(Mock<IUnitOfWork> unitOfWork)
        {
            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => new CommandService<object>(unitOfWork.Object, null));
        }

        [Theory]
        [AutoMoqData]
        public void Save_WithNullEntity_ThrowsArgumentNullException(Mock<IUnitOfWork> unitOfWork, Mock<IRepository> repository)
        {
            // Given
            var sut = new CommandService<object>(unitOfWork.Object, repository.Object);

            // When, Then
            Assert.Throws(typeof(ArgumentNullException), () => sut.Save(null));
        }

        [Theory]
        [AutoMoqData]
        public void Save_WithEntity_DelegatesToRepository(
            Mock<IUnitOfWork> unitOfWork,
            Mock<IRepository> repository,
            object entity)
        {
            // Given
            var sut = new CommandService<object>(unitOfWork.Object, repository.Object);

            // When
            sut.Save(entity);

            // Then
            repository.Verify(m => m.Save(entity));
        }
    }
}