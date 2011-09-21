using System;
using System.Web.Mvc;
using Thoughtology.Expresso.Services;

namespace Thoughtology.Expresso.Controllers
{
    public class PostsController
    {
        private readonly IPostQueryService postQueryService;

        public PostsController(IPostQueryService postQueryService)
        {
            if (postQueryService == null)
            {
                throw new ArgumentNullException("postQueryService");
            }

            this.postQueryService = postQueryService;
        }

        public object Index()
        {
            return new EmptyResult();
        }
    }
}