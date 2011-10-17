using System;
using System.Linq;
using Moq;
using Thoughtology.Expresso.Commands.Runtime;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;
using Common = Microsoft.Practices.ServiceLocation;

namespace Thoughtology.Expresso.Tests.Commands.Runtime
{
    public class ServiceLocatorTest
    {
        [Fact]
        public void GetServiceLocator_DoesNotReturnNull()
        {
            // When
            var result = ServiceLocator.Current;

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void GetServiceLocator_HasAllExpectedServices()
        {
            // Given
            var expectedServices = new[] { typeof(IUnitOfWork), typeof(IRepository<Post>) };

            // Then
            Assert.True(expectedServices.All(s => ServiceLocator.Current.GetService(s) != null));
        }

        [Fact]
        public void SetServiceLocator_WithNull_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws<ArgumentNullException>(() => ServiceLocator.SetCurrentInstance(null));
        }

        [Theory]
        [AutoMoqData]
        public void SetServiceLocator_WithServiceLocator_GetServiceLocatorReturnsSameInstance(
            Mock<Common.IServiceLocator> serviceLocator)
        {
            // When
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);
            var result = ServiceLocator.Current;

            // Then
            Assert.Same(serviceLocator.Object, result);
        }
    }
}
