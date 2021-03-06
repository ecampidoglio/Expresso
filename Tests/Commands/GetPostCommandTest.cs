﻿using System;
using System.Linq;
using System.Management.Automation;
using Moq;
using Thoughtology.Expresso.Commands;
using Thoughtology.Expresso.Commands.Runtime;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;
using Common = Microsoft.Practices.ServiceLocation;

namespace Thoughtology.Expresso.Tests.Commands
{
    public class GetPostCommandTest
    {
        [Fact]
        public void Constructor_SutIsCmdlet()
        {
            // When
            var sut = new GetPostCommand();

            // Then
            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

        [Fact]
        public void Class_HasCmdletAttribute()
        {
            // Given
            var expectedAttribute = new CmdletAttribute(VerbsCommon.Get, "Post");
            var sut = typeof(GetPostCommand);

            // When
            var result = sut.GetCustomAttributes(typeof(CmdletAttribute), false);

            // Then
            Assert.Contains(expectedAttribute, result);
        }

        [Fact]
        public void Invoke_DoesNotReturnNull()
        {
            // Given
            var sut = new GetPostCommand();

            // When
            var result = sut.Invoke();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_EagerLoadsAssociatedTags(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new GetPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Find("Tags"));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithNoPosts_ReturnsEmptySequenceOfPost(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new GetPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            var result = sut.Invoke<Post>();

            // Then
            Assert.False(result.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithSomePosts_ReturnsSameNumberOfPosts(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            Post[] posts)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<string[]>())).Returns(posts);
            var sut = new GetPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            var result = sut.Invoke<Post>();

            // Then
            Assert.Equal(posts.Count(), result.Count());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithException_ThrowsSameException(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            InvalidOperationException exception)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<string[]>())).Throws(exception);
            var sut = new GetPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // Then
            Assert.Throws(exception.GetType(), () => sut.Invoke<Post>().Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithExceptionAndInnerException_ThrowsInnerException(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string message,
            InvalidOperationException innerException)
        {
            // Given
            var exception = new Exception(message, innerException);
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Find(It.IsAny<string[]>())).Throws(exception);
            var sut = new GetPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // Then
            Assert.Throws(innerException.GetType(), () => sut.Invoke<Post>().Any());
        }
    }
}
