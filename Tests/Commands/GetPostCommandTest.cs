﻿using System.Linq;
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
        public void Invoke_WithNoPosts_ReturnsEmptySequenceOfPost(Mock<IServiceLocator> serviceLocator, Mock<IRepository<Post>> repository)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            var sut = new GetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            var result = sut.Invoke<Post>();

            // Then
            Assert.False(result.Any());
        }

        [Theory]
        [AutoMoqData]
        public void Invoke_WithSomePosts_ReturnsSameNumberOfPosts(Mock<IServiceLocator> serviceLocator, Mock<IRepository<Post>> repository, Post[] posts)
        {
            // Given
            serviceLocator.Setup(s => s.GetInstance<IRepository<Post>>()).Returns(repository.Object);
            repository.Setup(s => s.FindAll()).Returns(posts);
            var sut = new GetPostCommand();
            sut.SetServiceLocator(serviceLocator.Object);

            // When
            var result = sut.Invoke<Post>();

            // Then
            Assert.Equal(posts.Count(), result.Count());
        }
    }
}