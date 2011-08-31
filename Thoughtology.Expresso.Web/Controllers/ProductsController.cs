using System;
using System.Web.Mvc;

namespace Thoughtology.Expresso.Web.Controllers
{
    /// <summary>
    /// The controller responsible for displaying information related to products and categories.
    /// </summary>
    public class ProductsController : Controller
    {
        private readonly object productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductsController(object productService)
        {
            if (productService == null)
            {
                throw new ArgumentNullException("productService");
            }

            this.productService = productService;
        }

        /// <summary>
        /// Handles the default action for this controller.
        /// </summary>
        /// <returns>The view for the invoked action.</returns>
        public ActionResult Index()
        {
            return new EmptyResult();
        }

        /// <summary>
        /// Handles the <c>coffee</c> action for this controller.
        /// </summary>
        /// <returns>The view for the invoked action.</returns>
        public ActionResult Coffee()
        {
            return View();
        }
    }
}