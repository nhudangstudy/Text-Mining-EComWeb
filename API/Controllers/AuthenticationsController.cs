using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationsController(IAuthenticationService authenticationService) => _authenticationService = authenticationService;

        [HttpPost("check")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> IsValidAsync(CheckRequestAuthenticationModel checkRequestAuthentication)
        {
            return Ok(await _authenticationService.IsValidAsync(checkRequestAuthentication));
        }

        [HttpPost]
        public async Task<IActionResult> SendAsync(SendRequestAuthenticationModel sendRequestAuthentication)
        {
            await _authenticationService.SendAsync(sendRequestAuthentication);
            return Ok();
        }
    }
}
