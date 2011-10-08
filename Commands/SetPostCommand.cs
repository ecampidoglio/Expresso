using System;
using System.Linq;
using System.Management.Automation;
using Thoughtology.Expresso.Data;
using Thoughtology.Expresso.Model;

namespace Thoughtology.Expresso.Commands
{
    /// <summary>
    /// A PowerShell cmdlet that modifies an existing <see cref="Post"/> object in the data store.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "Post")]
    public class SetPostCommand : ServiceLocatorCommand
    {
        private IRepository<Post> repository;
        private Post activePost;

        /// <summary>
        /// Gets or sets the post to modify.
        /// </summary>
        [Parameter(
        ParameterSetName = "InputObject",
        Mandatory = true,
        ValueFromPipeline = true)]
        public Post InputObject { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the post to modify.
        /// </summary>
        [Parameter(
        ParameterSetName = "PostId",
        Mandatory = true,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true)]
        [Alias("PostId")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the post.
        /// </summary>
        [Parameter(Position = 0)]
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
                ModifyPost();
            }
            catch (Exception e)
            {
                ThrowError(e);
            }
        }

        private void ModifyPost()
        {
            SetActivePost();
            VerifyActivePostExists();
            ModifyActivePostFromParameters();
            SaveActivePost();
            SendActivePostToPipeline();
        }

        private void SetActivePost()
        {
            if (InputObject != null)
            {
                SetActivePostFromInputObject();
            }
            else
            {
                SetActivePostFromId();
            }
        }

        private void SetActivePostFromInputObject()
        {
            activePost = InputObject;
        }

        private void SetActivePostFromId()
        {
            InitializeRepository();
            activePost = repository.Find(p => p.Id == Id).SingleOrDefault();
        }

        private void InitializeRepository()
        {
            repository = ServiceLocator.GetInstance<IRepository<Post>>();
        }

        private void VerifyActivePostExists()
        {
            if (activePost == null)
            {
                throw new ArgumentException(String.Format("Could not find a post with ID '{0}'", Id));
            }
        }

        private void ModifyActivePostFromParameters()
        {
            SetTitleFromParameter();
            SetContentFromParameter();
        }

        private void SetTitleFromParameter()
        {
            if (Title != null)
            {
                activePost.Title = Title;
            }
        }

        private void SetContentFromParameter()
        {
            if (Content != null)
            {
                activePost.MarkdownContent = Content;
            }
        }

        private void SaveActivePost()
        {
            InitializeRepository();
            repository.Save(activePost);
        }

        private void SendActivePostToPipeline()
        {
            WriteObject(activePost);
        }
    }
}
