using System;
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
            sut.Dispose();
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
            // Given
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
        public void GetPosts_WithNewPost_ReturnsCollectionWithOneElement([Frozen] Post post, [Frozen] string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // When
            sut.RegisterAdded(post);
            sut.SaveChanges();
            var result = sut.Get<Post>();

            // Then
            Assert.Equal(1, result.Count());
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void RegisterAdded_WithNullPost_ThrowsArgumentNullException([Frozen] string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.RegisterAdded<Post>(null));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void RegisterAdded_WithNewPost_PersistsPost(
            [Frozen] string databaseName,
            Post post)
        {
            // Given
            var sut = new DataContext(databaseName);

            // When
            sut.RegisterAdded(post);
            sut.Commit();
            var result = sut.Get<Post>().FirstOrDefault();

            // Then
            Assert.Equal(post, result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void RegisterModified_WithNullPost_ThrowsArgumentNullException([Frozen] string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.RegisterModified<Post>(null));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void RegisterModified_WithModifiedPost_PersistsPost(
            [Frozen] string databaseName,
            Post post,
            string modifiedValue)
        {
            // Given
            var sut = new DataContext(databaseName);
            sut.RegisterAdded(post);
            sut.Commit();

            // When
            post.Title = modifiedValue;
            sut.RegisterModified(post);
            sut.Commit();
            var result = sut.Get<Post>().FirstOrDefault();

            // Then
            Assert.Equal(post, result);
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void RegisterDeleted_WithNullPost_ThrowsArgumentNullException([Frozen] string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.RegisterDeleted<Post>(null));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoData]
        public void RegisterDeleted_WithPost_RemovesPost(
            [Frozen] string databaseName,
            Post post)
        {
            // Given
            var sut = new DataContext(databaseName);
            sut.RegisterAdded(post);
            sut.Commit();

            // When
            sut.RegisterDeleted(post);
            sut.Commit();
            var result = sut.Get<Post>().Any();

            // Then
            Assert.False(result);
            sut.Database.Delete();
            sut.Dispose();
        }
    }
}
