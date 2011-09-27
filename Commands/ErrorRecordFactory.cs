using System;
using System.Management.Automation;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// Exposes static factory methods to easily create instances of the <see cref="ErrorRecord"/> class. 
    /// </summary>
    public static class ErrorRecordFactory
    {
        /// <summary>
        /// Creates an <see cref="ErrorRecord"/> object using the specified arguments.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> that is the source of error.</param>
        /// <param name="errorCategory">The optional error category. The default value is <see cref="F:ErrorCategory.NotSpecified"/></param>
        /// <param name="targetObject">The optional target object. The default value is <see langword="null"/>.</param>
        /// <returns>An <see cref="ErrorRecord"/> object.</returns>
        public static ErrorRecord CreateFromException(
            Exception exception,
            ErrorCategory errorCategory = ErrorCategory.NotSpecified,
            object targetObject = null)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            return new ErrorRecord(exception, exception.GetType().FullName, errorCategory, targetObject);
        }
    }
}