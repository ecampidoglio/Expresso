using System;
using System.Collections.Generic;
using System.Management.Automation;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// A PowerShell cmdlet that retrieves <see cref="Post"/> objects from the data store.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Post")]
    public class GetPostCommand : ServiceLocatorCommand
    {
        private IRepository<Post> repository;
        private IEnumerable<Post> posts;
        private ErrorRecord errorRecord;

        /// <summary>
        /// Processes each input object from the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                FindAllPosts();
                WriteObject(posts, enumerateCollection: true);
            }
            catch (Exception e)
            {
                ConvertExceptionToErrorRecord(e);
                ThrowTerminatingError(errorRecord);
            }
        }

        private void FindAllPosts()
        {
            InitializeRepository();
            posts = repository.FindAll();
        }

        private void InitializeRepository()
        {
            repository = ServiceLocator.GetInstance<IRepository<Post>>();
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

        private void CreateErrorRecordFromInnerException(Exception exception)
        {
            errorRecord = ErrorRecordFactory.CreateFromException(exception.InnerException, ErrorCategory.InvalidOperation);
        }

        private void CreateErrorRecordFromException(Exception exception)
        {
            errorRecord = ErrorRecordFactory.CreateFromException(exception);
        }
    }
}