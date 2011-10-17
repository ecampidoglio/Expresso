using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Tests.Foundation;
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
        [AutoMoqData]
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
        [AutoMoqData]
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
        [AutoMoqData]
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
        [AutoMoqData]
        public void GetPosts_WithNewPost_ReturnsCollectionWithOneElement(
            Post post,
            string databaseName)
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
        [AutoMoqData]
        public void GetPosts_WithNewPostAndTag_ReturnsPostWithTheSameTag(
            Post post,
            Tag tag,
            string databaseName)
        {
            // Given
            post.Tags.Add(tag);
            var sut = new DataContext(databaseName);

            // When
            sut.RegisterAdded(post);
            sut.SaveChanges();
            var result = sut.Get<Post>().SingleOrDefault();

            // Then
            Assert.True(post.Tags.SequenceEqual(result.Tags));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoMoqData]
        public void RegisterAdded_WithNullPost_ThrowsArgumentNullException(string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.RegisterAdded<Post>(null));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoMoqData]
        public void RegisterAdded_WithNewPost_PersistsPost(
            string databaseName,
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
        [AutoMoqData]
        public void RegisterModified_WithNullPost_ThrowsArgumentNullException(string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.RegisterModified<Post>(null));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoMoqData]
        public void RegisterModified_WithModifiedPost_PersistsPost(
            string databaseName,
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
        [AutoMoqData]
        public void RegisterDeleted_WithNullPost_ThrowsArgumentNullException(string databaseName)
        {
            // Given
            var sut = new DataContext(databaseName);

            // Then
            Assert.Throws<ArgumentNullException>(() => sut.RegisterDeleted<Post>(null));
            sut.Database.Delete();
            sut.Dispose();
        }

        [Theory]
        [AutoMoqData]
        public void RegisterDeleted_WithPost_RemovesPost(
            string databaseName,
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
