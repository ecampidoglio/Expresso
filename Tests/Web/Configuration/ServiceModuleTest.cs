using System.Linq;
using Autofac;
using Autofac.Core;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Services;
using Thoughtology.Expresso.Services.Configuration;
using Xunit;

namespace Thoughtology.Expresso.Tests.Web.Configuration
{
    public class ServiceModuleTest
    {
        [Fact]
        public void Constructor_SutIsModule()
        {
            // When
            var sut = new ServiceModule();

            // Then
            Assert.IsAssignableFrom<IModule>(sut);
        }

        [Fact]
        public void BuildContainer_ContainerHasAllExpectedServices()
        {
            // Given
            var expectedServices = new[]
            {
                typeof(IQueryService<Post>),
                typeof(IQueryService<Tag>),
                typeof(ICommandService<>)
            };
            var builder = new ContainerBuilder();
            var sut = new ServiceModule();

            // When
            builder.RegisterModule(sut);
            var container = builder.Build();

            // Then
            Assert.True(expectedServices.All(container.IsRegistered));
        }
    }
}
