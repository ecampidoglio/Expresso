using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Practices.ServiceLocation;
using Moq;
using Thoughtology.Expresso.Commands;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Commands
{
    public class ServiceLocatorCommandTest
    {
        [Fact]
        public void Constructor_SutIsCmdlet()
        {
            // When
            var sut = new ServiceLocatorCommand();

            // Then
            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

        [Fact]
        public void GetServiceLocator_DoesNotReturnNull()
        {
            // Given
            var sut = new ServiceLocatorCommand();

            // When
            var result = sut.ServiceLocator;

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void GetServiceLocator_HasAllExpectedServices()
        {
            // Given
            var expectedServices = new[]
            {
                typeof(IUnitOfWork),
                typeof(IRepository<Post>)
            };
            var sut = new ServiceLocatorCommand();

            // Then
            Assert.True(expectedServices.All(s => sut.ServiceLocator.GetService(s) != null));
        }

        [Fact]
        public void SetServiceLocator_WithNull_ThrowsArgumentNullException()
        {
            // Given
            var sut = new ServiceLocatorCommand();

            // When, Then
            Assert.Throws<ArgumentNullException>(() => sut.SetServiceLocator(null));
        }

        [Theory]
        [AutoMoqData]
        public void SetServiceLocator_WithServiceLocator_GetServiceLocatorReturnsSameInstance(Mock<IServiceLocator> serviceLocator)
        {
            // Given
            var sut = new ServiceLocatorCommand();

            // When
            sut.SetServiceLocator(serviceLocator.Object);
            var result = sut.ServiceLocator;

            // Then
            Assert.Same(serviceLocator.Object, result);
        }
    }
}