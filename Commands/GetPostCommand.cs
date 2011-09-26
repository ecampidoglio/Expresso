using System.Collections.Generic;
using System.Management.Automation;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Commands
{
    [Cmdlet(VerbsCommon.Get, "Post")]
    public class GetPostCommand : ServiceLocatorCommand
    {
        private IRepository<Post> repository;

        protected override void ProcessRecord()
        {
            var posts = FindAllPosts();
            WriteObject(posts, true);
        }

        private IEnumerable<Post> FindAllPosts()
        {
            InitializeRepository();
            var posts = repository.FindAll();

            return posts;
        }

        private void InitializeRepository()
        {
            repository = ServiceLocator.GetInstance<IRepository<Post>>();
        }
    }
}