using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandsController(IBrandService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int size, int? page = 1)
        {

            var brands = _service.GetAllAsync(size, page);
            return Ok(brands);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateBrandModel model)
        {
            await _service.AddAsync(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateBrandModel model)
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
