using System;
using System.Management.Automation;
using Ploeh.AutoFixture.Xunit;
using Thoughtology.Expresso.Commands;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Commands
{
    public class ErrorRecordFactoryTest
    {
        [Theory]
        [AutoData]
        public void CreateFromException_WithNullException_ThrowsArgumentNullException()
        {
            // When, Then
            Assert.Throws<ArgumentNullException>(() => ErrorRecordFactory.CreateFromException(null));
        }

        [Theory]
        [AutoData]
        public void CreateFromException_WithException_DoesNotReturnNull(Exception exception)
        {
            // When
            var result = ErrorRecordFactory.CreateFromException(exception);

            // Then
            Assert.NotNull(result);
        }

        [Theory]
        [AutoData]
        public void CreateFromException_WithException_ReturnsErrorRecordWithSameException(Exception exception)
        {
            // When
            var result = ErrorRecordFactory.CreateFromException(exception);

            // Then
            Assert.Same(exception, result.Exception);
        }

        [Theory]
        [AutoData]
        public void CreateFromException_WithException_ReturnsErrorRecordWithExceptionFullTypeNameAsErrorId(Exception exception)
        {
            // Given
            var expectedErrorId = exception.GetType().FullName;

            // When
            var result = ErrorRecordFactory.CreateFromException(exception);

            // Then
            Assert.Equal(expectedErrorId, result.FullyQualifiedErrorId);
        }

        [Theory]
        [AutoData]
        public void CreateFromException_WithException_ReturnsErrorRecordWithErrorCategoryNotSpecified(Exception exception)
        {
            // Given
            var expectedErrorCategory = ErrorCategory.NotSpecified;

            // When
            var result = ErrorRecordFactory.CreateFromException(exception);

            // Then
            Assert.Equal(expectedErrorCategory, result.CategoryInfo.Category);
        }

        [Theory]
        [InlineAutoData(ErrorCategory.PermissionDenied)]
        [InlineAutoData(ErrorCategory.SyntaxError)]
        [InlineAutoData(ErrorCategory.OperationTimeout)]
        public void CreateFromException_WithExceptionAndErrorCategory_ReturnsErrorRecordWithSameErrorCategory(
        ErrorCategory errorCategory,
        Exception exception)
        {
            // When
            var result = ErrorRecordFactory.CreateFromException(exception, errorCategory);

            // Then
            Assert.Equal(errorCategory, result.CategoryInfo.Category);
        }

        [Theory]
        [AutoData]
        public void CreateFromException_WithExceptionAndTarget_ReturnsErrorRecordWithSameTarget(
        Exception exception,
        object target)
        {
            // When
            var result = ErrorRecordFactory.CreateFromException(exception, targetObject: target);

            // Then
            Assert.Same(target, result.TargetObject);
        }
    }
}
