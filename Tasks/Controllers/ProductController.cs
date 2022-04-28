using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService todoService)
        {
            _productService = todoService;

        }
        [HttpGet]
        [Route("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
