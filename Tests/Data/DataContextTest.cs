using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Ploeh.AutoFixture.Xunit;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Data
{
    public class DataContextTest
    {
        public DataContextTest()
        {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
        }

        [Fact]
        public void Constructor_SutIsUnitOfWork()
        {
            // When
            var sut = new DataContext();

            // Then
            Assert.IsAssignableFrom<IUnitOfWork>(sut);
        }

        [Theory]
        [AutoData]
        public void Constructor_DoesNotThrow(string databaseName)
        {
            DataContext sut = null;

            Assert.DoesNotThrow(() => sut = new DataContext(databaseName));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_DoesNotReturnNull(string databaseName)
        {
            var sut = new DataContext(databaseName);

            var result = sut.Get<Post>();

            Assert.NotNull(result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_ReturnsEmptyCollection(string databaseName)
        {
            var sut = new DataContext(databaseName);

            var result = sut.Get<Post>();

            Assert.Empty(result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_WithNewItem_ReturnsCollectionWithOneElement([Frozen]Post post, [Frozen]string databaseName)
        {
            var sut = new DataContext(databaseName);

            sut.Get<Post>().Add(post);
            sut.SaveChanges();
            var result = sut.Get<Post>();

            Assert.Equal(1, result.Count());
            sut.Database.Delete();
            sut.Dispose();
        }
    }
}