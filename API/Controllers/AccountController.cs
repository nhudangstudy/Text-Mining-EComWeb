using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService) => _accountService = accountService;

        [HttpGet("last-name")]
        [ProducesResponseType(typeof(GetLastNameByIdAccountModel), 200)]
        [Authorize]
        public async Task<IActionResult> GetLastNameByIdAsync()
        {
            return Ok(await _accountService.GetLastNameByIdAccountAsync());
        }

        [HttpGet("is-exist/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> IsExistAsync(string id)
        {
            return Ok(await _accountService.IsExistAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetByIdPublicAccountModel), 200)]
        public async Task<IActionResult> RegisterAsync(RegisterRequestAccountModel registerRequestAccount)
        {
            await _accountService.RegisterAsync(registerRequestAccount);
            return StatusCode(201);
        }

        [HttpPost("active")]
        public async Task<IActionResult> ActiveAsync(CheckRequestAuthenticationModel checkRequestAuthentication)
        {
            await _accountService.ActiveAsync(checkRequestAuthentication);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateFullNameAsync(string id, string fullName)
        {
            await _accountService.UpdateFullNameAsync(id, fullName);
            return Ok();
        }

        [HttpPatch("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestAccountModel resetPasswordRequestAccount)
        {
            await _accountService.ResetPasswordAsync(resetPasswordRequestAccount);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _accountService.DeleteAsync(id);
            return StatusCode(204);
        }
    }
}
