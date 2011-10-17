using System;
using System.Management.Automation;
using Thoughtology.Expresso.Commands.Runtime;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// A PowerShell cmdlet that creates a new <see cref="Post"/> object in the data store.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "Post")]
    public class NewPostCommand : Command
    {
        private IRepository<Post> repository;
        private Post newPost;

        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the post.
        /// </summary>
        [Parameter(
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the tags of the post.
        /// </summary>
        [Parameter(
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public string[] Tags { get; set; }

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
            newPost = new Post { Title = Title, MarkdownContent = Content };
            AddTagsFromParameters();
        }

        private void AddTagsFromParameters()
        {
            if (Tags != null)
            {
                foreach (var tag in Tags)
                {
                    newPost.Tags.Add(new Tag { Name = tag });
                }
            }
        }

        private void SaveNewPost()
        {
            InitializeRepository();
            repository.Save(newPost);
        }

        private void InitializeRepository()
        {
            repository = ServiceLocator.Current.GetInstance<IRepository<Post>>();
        }

        private void SendNewPostToPipeline()
        {
            WriteObject(newPost);
        }
    }
}
