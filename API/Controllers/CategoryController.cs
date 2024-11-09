using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var category = await _service.GetByIdAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int size, int? page = 1)
        {
            var categories = _service.GetAllAsync(size, page);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCategoryModel model)
        {
            await _service.AddAsync(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateCategoryModel model)
        {
            await _service.UpdateAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteAsync(int id)
        {
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
    }

}
