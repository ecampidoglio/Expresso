using System;
using System.Management.Automation;
using Thoughtology.Expresso.Commands.Runtime;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// Represents the base class for all cmdlets.
    /// </summary>
    public abstract class Command : Cmdlet
    {
        private ErrorRecord errorRecord;

        /// <summary>
        /// Throws the specified <see cref="Exception"/> as a terminating error for the cmdlet.
        /// </summary>
        /// <param name="e">The <see cref="Exception"/> to throw.</param>
        protected void ThrowError(Exception e)
        {
            ConvertExceptionToErrorRecord(e);
            ThrowTerminatingError(errorRecord);
        }

        private void ConvertExceptionToErrorRecord(Exception exception)
        {
            if (exception.InnerException != null)
            {
                CreateErrorRecordFromInnerException(exception);
            }
            else
            {
                CreateErrorRecordFromException(exception);
            }
        }

        private void CreateErrorRecordFromException(Exception exception)
        {
            errorRecord = ErrorRecordFactory.CreateFromException(exception);
        }

        private void CreateErrorRecordFromInnerException(Exception exception)
        {
            errorRecord = ErrorRecordFactory.CreateFromException(exception.InnerException, ErrorCategory.InvalidOperation);
        }
    }
}
