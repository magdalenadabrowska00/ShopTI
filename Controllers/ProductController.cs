using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShopTI.Entities;
using ShopTI.IServices;
using ShopTI.Models;

namespace ShopTI.Controllers
{
    [Route("api/product")]
    [ApiController]
    [EnableCors]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("newProduct")]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateProduct([FromBody] CreateProductModel newProduct)
        {
            var newProductId = _productService.CreateProduct(newProduct);
            return Created($"{newProductId}", null);
        }

        [HttpGet("getProducts")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return _productService.GetProducts();
        }
    }
}
