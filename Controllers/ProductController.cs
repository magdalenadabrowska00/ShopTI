using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [EnableCors]
    [Authorize]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateProduct([FromForm] CreateProductModel newProduct)
        {
            var newProductId = _productService.CreateProduct(newProduct);
            return Created($"{newProductId}", null);
        }
    }
}
