using System;
using System.Web.Mvc;
using Thoughtology.Expresso.Tests.Foundation;
using Thoughtology.Expresso.Web.Controllers;
using Xunit;
using Xunit.Extensions;

namespace Thoughtology.Expresso.Tests.Web.Controllers
{
    public class ProductsControllerTest
    {
        [Fact]
        public void Constructor_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new ProductsController(null));
        }

        [Theory]
        [AutoMoqData]
        public void Index_DoesNotReturnNull(object productService)
        {
            var sut = new ProductsController(productService);

            var result = sut.Index();

            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Index_ReturnsEmptyResult(object productService)
        {
            var sut = new ProductsController(productService);

            var result = sut.Index();

            Assert.IsType<EmptyResult>(result);
        }

        [Theory]
        [AutoMoqData]
        public void Coffee_DoesNotReturnNull(object productService)
        {
            var sut = new ProductsController(productService);

            var result = sut.Coffee();

            Assert.NotNull(result);
        }

        [Theory]
        [AutoMoqData]
        public void Coffee_ReturnsViewResult(object productService)
        {
            var sut = new ProductsController(productService);

            var result = sut.Coffee();

            Assert.IsType<ViewResult>(result);
        }
    }
}