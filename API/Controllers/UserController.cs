using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace API.Controllers
{

    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("{email}")]
        [ProducesResponseType(typeof(GetByIdPublicAccountModel), 200)]
        public async Task<IActionResult> CreateUserAync(string email, CreateUpdateUserRequestModel createUserRequest)
        {
            await _userService.CreateNewUser(email, createUserRequest);
            return Ok();
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserById(string email)
        {
            var user = await _userService.GetUserByIdAsync(email);
            if (user is not null)
            {
                return Ok(user);

            }
            else
            {
                return NotFound(new { Message = "Product not found." });
            }
        }
    }
}
