using System;
using System.Web.Mvc;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Services;

namespace Thoughtology.Expresso.Controllers
{
    public class PostsController : Controller
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
            ViewBag.Posts = new Post[0];

            return View();
        }
    }
}