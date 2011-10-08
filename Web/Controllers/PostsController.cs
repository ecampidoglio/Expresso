using System;
using System.Web.Mvc;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Services;

namespace Thoughtology.Expresso.Controllers
{
    /// <summary>
    /// Handles the interactions with the posts view.
    /// </summary>
    public class PostsController : Controller
    {
        private readonly IQueryService<Post> postQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostsController"/> class.
        /// </summary>
        /// <param name="postQueryService">The query service used to retrieve the <see cref="Post"/> items to display.</param>
        /// <exception cref="ArgumentNullException"><paramref name="postQueryService"/> is null.</exception>
        public PostsController(IQueryService<Post> postQueryService)
        {
            if (postQueryService == null)
            {
                throw new ArgumentNullException("postQueryService");
            }

            this.postQueryService = postQueryService;
        }

        /// <summary>
        /// Handles the default action for this controller
        /// </summary>
        /// <returns>The view to render.</returns>
        public ActionResult Index()
        {
            ViewBag.Posts = postQueryService.Find();

            return View();
        }
    }
}
