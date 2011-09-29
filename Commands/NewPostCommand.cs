using System;
using System.Management.Automation;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// A PowerShell cmdlet that creates a new <see cref="Post"/> object in the data store.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "Post")]
    public class NewPostCommand : ServiceLocatorCommand
    {
        private IRepository<Post> repository;
        private Post newPost;

        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the post.
        /// </summary>
        [Parameter(Position = 1)]
        public string Content { get; set; }

        /// <summary>
        /// Processes each input object from the pipeline.
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                CreateNewPost();
            }
            catch (Exception e)
            {
                ThrowError(e);
            }
        }

        private void CreateNewPost()
        {
            VerifyParameters();
            CreateNewPostFromParameters();
            SaveNewPost();
            SendNewPostToPipeline();
        }

        private void VerifyParameters()
        {
            if (string.IsNullOrEmpty(Title))
            {
                throw new ArgumentException("Title is required", "Title");
            }
        }

        private void CreateNewPostFromParameters()
        {
            newPost = new Post
            {
                Title = Title,
                MarkdownContent = Content
            };
        }

        private void SaveNewPost()
        {
            InitializeRepository();
            repository.Save(newPost);
        }

        private void InitializeRepository()
        {
            repository = ServiceLocator.GetInstance<IRepository<Post>>();
        }

        private void SendNewPostToPipeline()
        {
            WriteObject(newPost);
        }
    }
}
