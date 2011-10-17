using System;
using System.Linq;
using Thoughtology.Expresso.Commands.Runtime;
using Thoughtology.Expresso.Tests.Foundation;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Commands.Runtime
{
    public class PSCmdletExtensionTest
    {
        [Fact]
        public void InvokeInRunspace_WithNoParameters_DoesNotReturnNull()
        {
            // Given
            var sut = new WriteInputCommand();

            // When
            var result = sut.InvokeInRunspace();

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("String")]
        [InlineData(default(int))]
        [InlineData(default(bool))]
        public void InvokeInRunspace_WithParameter_ReturnsParameterValue(object parameterValue)
        {
            // Given
            var cmdlet = new WriteInputCommand { InputObject = parameterValue };

            // When
            var result = PSCmdletExtension.InvokeInRunspace(cmdlet)
                .Cast<object>()
                .SingleOrDefault();

            // Then
            Assert.Equal(parameterValue, result);
        }

        [Fact]
        public void InvokeInRunspace_WithNull_ThrowsArgumentNullException()
        {
            // Given, Then
            Assert.Throws<ArgumentNullException>(() => PSCmdletExtension.InvokeInRunspace(null));
        }

        [Theory]
        [InlineData("String")]
        [InlineData(default(int))]
        [InlineData(default(bool))]
        public void InvokeInRunspace_WithTypeParameter_ReturnsParameterValue(object parameterValue)
        {
            // Given
            var cmdlet = new WriteInputCommand { InputObject = parameterValue };

            // When
            var result = PSCmdletExtension.InvokeInRunspace<object>(cmdlet)
                .SingleOrDefault();

            // Then
            Assert.Equal(parameterValue, result);
        }

        [Fact]
        public void InvokeInRunspace_WithTypeParameterAndNull_ThrowsArgumentNullException()
        {
            // Given, Then
            Assert.Throws<ArgumentNullException>(() => PSCmdletExtension.InvokeInRunspace<object>(null));
        }
    }
}
