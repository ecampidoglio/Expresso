using System;
using System.Linq;
using System.Web.Mvc;
using Thoughtology.Expresso.Model;
using Thoughtology.Expresso.Services;

namespace Thoughtology.Expresso.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly IQueryService<Tag> tagQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="tagQueryService">The query service used to retrieve the <see cref="Tag"/> items to display.</param>
        /// <exception cref="ArgumentNullException"><paramref name="tagQueryService"/> is null.</exception>
        public TagController(IQueryService<Tag> tagQueryService)
        {
            if (tagQueryService == null)
            {
                throw new ArgumentNullException("tagQueryService");
            }

            this.tagQueryService = tagQueryService;
        }

        /// <summary>
        /// Handles the default action for this controller.
        /// </summary>
        /// <param name="id">The identifier of the tag to display.</param>
        /// <returns>The view to render.</returns>
        public ViewResult Index(string id)
        {
            ViewData.Model = TagNamed(id).Posts;
            return View();
        }

        private Tag TagNamed(string name)
        {
            return this.tagQueryService
                       .Find()
                       .Single(t => t.Name == name);
        }
    }
}
