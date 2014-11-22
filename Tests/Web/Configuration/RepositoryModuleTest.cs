using System.Linq;
using Autofac;
using Autofac.Core;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Data.Configuration;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Web.Configuration
{
    public class RepositoryModuleTest
    {
        [Theory, AutoMoqData]
        public void Constructor_SutIsModule(RepositoryModule sut)
        {
            // Then
            Assert.IsAssignableFrom<IModule>(sut);
        }

        [Theory, AutoMoqData]
        public void BuildContainer_ContainerHasAllExpectedServices(
            ContainerBuilder builder,
            RepositoryModule sut)
        {
            // Given
            var expectedServices = new[] { typeof(IUnitOfWork), typeof(IRepository<>) };

            // When
            builder.RegisterModule(sut);
            var container = builder.Build();

            // Then
            Assert.True(expectedServices.All(container.IsRegistered));
        }
    }
}
