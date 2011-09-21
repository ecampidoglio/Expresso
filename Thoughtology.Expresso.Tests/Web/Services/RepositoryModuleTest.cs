using Xunit;
using System;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using System.Linq;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Data;

namespace Thoughtology.Expresso.Tests.Web.Services
{
    public class RepositoryModule : IModule
    {
        public void Configure(IComponentRegistry componentRegistry)
        {
            throw new NotImplementedException();
        }
    }

    public class RepositoryModuleTest
    {
        [Fact]
        public void Constructor_RegistersOpenRepositories()
        {
            // Given
            var builder = new ContainerBuilder();
            var sut = new RepositoryModule();

            // When
            builder.RegisterModule(sut);
            var container = builder.Build();
            var postRepo = container.Resolve<IRepository<Post>>();

            // Then
            Assert.NotNull(postRepo);
        }
    }
}