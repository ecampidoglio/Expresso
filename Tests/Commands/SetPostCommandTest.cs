using System;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Practices.ServiceLocation;
using Moq;
using Thoughtology.Expresso.Commands;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Commands
{
    public class SetPostCommandTest
    {
        [Fact]
        public void Constructor_SutIsCmdlet()
        {
            // When
            var sut = new SetPostCommand();

            // Then
            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

        [Fact]
        public void Invoke_DoesNotReturnNull()
        {
            // Given
            var sut = new SetPostCommand();

            // When
            var result = sut.Invoke();

            // Then
            Assert.NotNull(result);
        }

        [Fact]
        public void Class_HasCmdletAttribute()
        {
            // Given
            var expectedAttribute = new CmdletAttribute(VerbsCommon.Set, "Post");
            var sut = typeof(SetPostCommand);

            // When
            var result = sut.GetCustomAttributes(typeof(CmdletAttribute), false);

            // Then
            Assert.Contains(expectedAttribute, result);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObject_DoesNotReturnNull()
        {
            // Given
            var sut = new SetPostCommand();

            // When
            var result = sut.Invoke<Post>();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObject_ReturnsInputObject(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = post;
            var result = sut.Invoke<Post>().SingleOrDefault();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithId_ReturnsPostWithSameId(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            var result = sut.Invoke<Post>().SingleOrDefault();

            // Then
            Assert.Equal(post.Id, result.Id);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObject_SavesInputObject(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = post;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(post));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithNullInputObjectAndId_DoesNotThrow(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = null;
            sut.Id = post.Id;

            // Then
            Assert.DoesNotThrow(() => sut.Invoke<Post>().Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObjectAndTitle_SavesInputObjectWithNewTitle(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post,
            string title)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = post;
            sut.Title = title;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.Title == title)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObjectAndNullTitle_SavesInputObjectWithNotNullTitle(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = post;
            sut.Title = null;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.Title != null)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObjectAndContent_SavesInputObjectWithNewContent(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post,
            string content)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = post;
            sut.Content = content;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.MarkdownContent == content)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObjectAndNullContent_SavesInputObjectWithNotNullContent(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = post;
            sut.Content = null;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.MarkdownContent != null)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithId_SavesPostWithSameId(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(post));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdAndTitle_SavesPostWithSameIdAndTitle(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post,
            string title)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Title = title;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.Title == title)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdAndNullTitle_SavesPostWithSameIdAndNotNullTitle(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Title = null;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.Title != null)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdAndContent_SavesPostWithSameIdAndContent(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post,
            string content)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Content = content;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.MarkdownContent == content)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdAndNullContent_SavesPostWithSameIdAndNotNullContent(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Content = null;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && p.MarkdownContent != null)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdAndSingleTag_SavesPostWithSameIdAndTag(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post,
            string tag)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Tags = new[] { tag };
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id &&
                     p.Tags.Select(t => t.Name).FirstOrDefault() == tag)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdAndMultipleTags_SavesPostWithSameIdAndTags(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post,
            string[] tags)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Tags = tags;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id &&
                     p.Tags.Select(t => t.Name).SequenceEqual(tags))));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdAndNullTags_SavesPostWithSameIdAndEmptyTags(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post)
        {
            // Given
            var posts = new[] { post };
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Returns(posts);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = post.Id;
            sut.Tags = null;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(
                p => p.Id == post.Id && !p.Tags.Any())));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithInputObjectAndId_SavesInputObject(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post post,
            int anotherPostId)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.InputObject = post;
            sut.Id = anotherPostId;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(post));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdThatDoesNotExist_ThrowsArgumentException(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            int postId)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = postId;

            // Then
            Assert.Throws<ArgumentException>(() => sut.Invoke<Post>().Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithIdThatDoesNotExistAndTitle_ThrowsArgumentException(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            int postId,
            string title)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = postId;
            sut.Title = title;

            // Then
            Assert.Throws<ArgumentException>(() => sut.Invoke<Post>().Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithException_ThrowsSameException(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            InvalidOperationException exception,
            int postId)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Throws(exception);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = postId;

            // Then
            Assert.Throws(exception.GetType(), () => sut.Invoke<Post>().Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithExceptionAndInnerException_ThrowsInnerException(
            Mock<IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            int postId,
            string message,
            InvalidOperationException innerException)
        {
            // Given
            var exception = new Exception(message, innerException);
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<Expression<Func<Post, bool>>>())).Throws(exception);
            var sut = new SetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            sut.Id = postId;

            // Then
            Assert.Throws(innerException.GetType(), () => sut.Invoke<Post>().Any());
        }
    }
}
