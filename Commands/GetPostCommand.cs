using System;
using System.Collections.Generic;
using System.Management.Automation;
using Thoughtology.Expresso.Commands.Runtime;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// A PowerShell cmdlet that retrieves <see cref="Post"/> objects from the data store.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Post")]
    public class GetPostCommand : Command
    {
        private IRepository<Post> repository;
        private IEnumerable<Post> posts;

        /// <summary>
        /// Processes each input object from the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                FindAllPosts();
                SendPostsToPipeline();
            }
            catch (Exception e)
            {
                ThrowError(e);
            }
        }

        private void FindAllPosts()
        {
            InitializeRepository();
            posts = repository.Find("Tags");
        }

        private void InitializeRepository()
        {
            repository = ServiceLocator.Current.GetInstance<IRepository<Post>>();
        }

        private void SendPostsToPipeline()
        {
            WriteObject(posts, enumerateCollection: true);
        }
    }
}
