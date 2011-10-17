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
    public class NewPostCommandTest
    {
        [Fact]
        public void Constructor_SutIsCmdlet()
        {
            // When
            var sut = new NewPostCommand();

            // Then
            Assert.IsAssignableFrom<Cmdlet>(sut);
        }

        [Fact]
        public void Class_HasCmdletAttribute()
        {
            // Given
            var expectedAttribute = new CmdletAttribute(VerbsCommon.New, "Post");
            var sut = typeof(NewPostCommand);

            // When
            var result = sut.GetCustomAttributes(typeof(CmdletAttribute), false);

            // Then
            Assert.Contains(expectedAttribute, result);
        }

        [Fact]
        public void Invoke_DoesNotReturnNull()
        {
            // Given
            var sut = new NewPostCommand();

            // When
            var result = sut.Invoke();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithTitle_DelegatesToRepository(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = title;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(p => p.Title == title)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithNullTitle_ThrowsArgumentException(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = null;

            // Then
            Assert.Throws<ArgumentException>(() => sut.Invoke<Post>().Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithContent_DelegatesToRepository(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title,
            string content)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = title;
            sut.Content = content;
            sut.Invoke<Post>().Any();

            // Then
            repository.Verify(m => m.Save(It.Is<Post>(p => p.MarkdownContent == content)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithTitle_ReturnsSinglePost(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = title;
            var result = sut.Invoke<Post>().SingleOrDefault();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithTitle_ReturnsSinglePostWithSameTitle(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = title;
            var result = sut.Invoke<Post>().SingleOrDefault();

            // Then
            Assert.Equal(title, result.Title);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithContent_ReturnsSinglePostWithSameContent(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title,
            string content)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = title;
            sut.Content = content;
            var result = sut.Invoke<Post>().SingleOrDefault();

            // Then
            Assert.Equal(content, result.MarkdownContent);
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithTags_ReturnsSinglePostWithSameTags(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title,
            string[] tags)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = title;
            sut.Tags = tags;
            var result = sut.Invoke<Post>().SingleOrDefault();

            // Then
            Assert.True(tags.SequenceEqual(result.Tags.Select(t => t.Name)));
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithNullTags_ReturnsSinglePostWithEmptyTags(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);

            // When
            sut.Title = title;
            sut.Tags = null;
            var result = sut.Invoke<Post>().SingleOrDefault();

            // Then
            Assert.False(result.Tags.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithException_ThrowsSameException(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            InvalidOperationException exception,
            string title)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Save(It.IsAny<Post>())).Throws(exception);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);
            sut.Title = title;

            // Then
            Assert.Throws(exception.GetType(), () => sut.Invoke<Post>().Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithExceptionAndInnerException_ThrowsInnerException(
            Mock<Common.IServiceLocator> serviceLocator,
            Mock<IRepository<Post>> repository,
            string title,
            string message,
            InvalidOperationException innerException)
        {
            // Given
            var exception = new Exception(message, innerException);
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.Save(It.IsAny<Post>())).Throws(exception);
            var sut = new NewPostCommand();
            ServiceLocator.SetCurrentInstance(serviceLocator.Object);
            sut.Title = title;

            // Then
            Assert.Throws(innerException.GetType(), () => sut.Invoke<Post>().Any());
        }
    }
}
