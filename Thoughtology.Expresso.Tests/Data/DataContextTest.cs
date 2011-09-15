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

            var result = sut.Posts;

            Assert.NotNull(result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_ReturnsEmptyCollection(string databaseName)
        {
            var sut = new DataContext(databaseName);

            var result = sut.Posts;

            Assert.Empty(result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_WithNewItem_ReturnsCollectionWithOneElement([Frozen]Post post, [Frozen]string databaseName)
        {
            var sut = new DataContext(databaseName);

            sut.Posts.Add(post);
            sut.SaveChanges();
            var result = sut.Posts;

            Assert.Equal(1, result.Count());
            sut.Database.Delete();
            sut.Dispose();
        }
    }
}