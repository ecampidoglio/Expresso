using System;
using System.Web.Mvc;

namespace Thoughtology.Expresso.Controllers
{
    public class PostsController
    {
        private readonly object postService;

        public PostsController(object postService)
        {
            if (postService == null)
            {
                throw new ArgumentNullException("postService");
            }

            this.postService = postService;
        }

        public object Index()
        {
            return new EmptyResult();
        }
    }
}