using Xunit;
using System;

namespace Thoughtology.Expresso.Tests.Web.Controllers
{
    public class ProductsController
    {
        public ProductsController(object productService)
        {
            throw new NotImplementedException();
        }
    }

    public class ProductsControllerTest
    {
        [Fact]
        public void Constructor_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new ProductsController(null));
        }
    }
}