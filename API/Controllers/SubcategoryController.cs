using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _service;

        public SubCategoriesController(ISubCategoryService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var subCategory = await _service.GetByIdAsync(id);
            return subCategory == null ? NotFound() : Ok(subCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var subCategories = await _service.GetAllAsync();
            return Ok(subCategories);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateSubCategoryModel model)
        {
            await _service.CreateAsync(model);
            return Ok(model);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateSubCategoryModel model)
        {
            await _service.UpdateAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
