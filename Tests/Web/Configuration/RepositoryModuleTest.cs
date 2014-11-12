using System.Linq;
using Autofac;
using Autofac.Core;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Data.Configuration;
using Xunit;

namespace Thoughtology.Expresso.Tests.Web.Configuration
{
    public class RepositoryModuleTest
    {
        [Fact]
        public void Constructor_SutIsModule()
        {
            // When
            var sut = new RepositoryModule();

            // Then
            Assert.IsAssignableFrom<IModule>(sut);
        }

        [Fact]
        public void BuildContainer_ContainerHasAllExpectedServices()
        {
            // Given
            var expectedServices = new[] { typeof(IUnitOfWork), typeof(IRepository<>) };
            var builder = new ContainerBuilder();
            var sut = new RepositoryModule();

            // When
            builder.RegisterModule(sut);
            var container = builder.Build();

            // Then
            Assert.True(expectedServices.All(s => container.IsRegistered(s)));
        }
    }
}
