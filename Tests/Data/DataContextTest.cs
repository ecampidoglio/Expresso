using System;
using System.Data;
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
            // When
            DataContext sut = null;

            // Then
            Assert.DoesNotThrow(() => sut = new DataContext(databaseName));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_DoesNotReturnNull(string databaseName)
        {
            //  Given
            var sut = new DataContext(databaseName);

            // When
            var result = sut.Get<Post>();

            // Then
            Assert.NotNull(result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_ReturnsEmptyCollection(string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // When
            var result = sut.Get<Post>();

            // Then
            Assert.Empty(result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPosts_WithNewItem_ReturnsCollectionWithOneElement([Frozen] Post post, [Frozen] string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // When
            sut.Get<Post>().Add(post);
            sut.SaveChanges();
            var result = sut.Get<Post>();

            // Then
            Assert.Equal(1, result.Count());
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPostState_WithNullItem_ThrowsArgumentNullException([Frozen] string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.GetState<Post>(null));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPostState_WithDetachedItem_ReturnsDetachedState([Frozen] Post post, [Frozen] string databaseName)
        {
            // Given
            var expectedState = EntityState.Detached;
            var sut = new DataContext(databaseName);

            // When
            EntityState result = sut.GetState<Post>(post);

            // Then
            Assert.Equal(expectedState, result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void GetPostState_WithTransientItem_ReturnsDetachedState([Frozen] Post post, [Frozen] string databaseName)
        {
            // Given
            post.Id = 0;
            var expectedState = EntityState.Detached;
            var sut = new DataContext(databaseName);

            // When
            EntityState result = sut.GetState<Post>(post);

            // Then
            Assert.Equal(expectedState, result);
            sut.Database.Delete();
            sut.Dispose();
        }
    }
}
