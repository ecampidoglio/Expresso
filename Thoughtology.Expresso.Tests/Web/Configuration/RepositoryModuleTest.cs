using Autofac;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Web.Configuration;
using Xunit;

namespace Thoughtology.Expresso.Tests.Web.Configuration
{
    public class RepositoryModuleTest
    {
        [Fact]
        public void BuildContainerAndResolvePostRepository_DoesNotReturnNull()
        {
            // Given
            var builder = new ContainerBuilder();
            var sut = new RepositoryModule();

            // When
            builder.RegisterModule(sut);
            var container = builder.Build();
            var repository = container.Resolve<IRepository<Post>>();

            // Then
            Assert.NotNull(repository);
        }
    }
}