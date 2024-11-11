using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("public")]
        public async Task<IActionResult> GetAllProductsPublicAsync()
        {
            var products = await _service.GetAllProductsPublicAsync();
            return Ok(products);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAllProductsAdminAsync()
        {
            var products = await _service.GetAllProductsAdminAsync();
            return Ok(products);
        }

        [HttpPut("{asin}")]
        public async Task<IActionResult> UpdateProductAsync(string asin, CreateProductRequestModel model)
        {
            try
            {
                await _service.UpdateProductAsync(asin, model);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Product not found." });
            }
        }

        [HttpGet("{asin}")]
        public async Task<ProductResponseModel> GetProductById(string asin)
        {
                var product = await _service.GetProductById(asin);
                return product;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsync(int size, int? page = 1)
        {

            var products = _service.GetAllProductAsync(size, page);
            return Ok(products);
        }

        [HttpPut("{asin}/price")]
        public async Task<IActionResult> UpdateProductPriceAsync(string asin, CreateProductPriceModel priceModel)
        {
            try
            {
                await _service.UpdateProductPriceAsync(asin, priceModel);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Product not found." });
            }
        }
    }

}
