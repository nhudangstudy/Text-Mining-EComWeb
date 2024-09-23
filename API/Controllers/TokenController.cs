using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService) => _tokenService = tokenService;

        [HttpPost]
        [ProducesResponseType(typeof(GetByLoginTokenModel), 200)]
        public async Task<IActionResult> GetTokenAsync(LoginRequestAccountModel loginAccount, bool? includeRefreshToken)
        {
            var token = await _tokenService.GetByLoginAsync(loginAccount, includeRefreshToken);
            return Ok(token);
        }

        [HttpPost("google-auth")]
        [ProducesResponseType(typeof(GetByLoginTokenModel), 200)]
        public async Task<IActionResult> GetTokenAsync([FromBody] string accessToken, bool? includeRefreshToken)
        {
            var token = await _tokenService.GetByLoginAsync(accessToken, includeRefreshToken);
            return Ok(token);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AccessTokenModel), 200)]
        public async Task<IActionResult> GetTokenAsync(GetAccessByRefreshRequestTokenModel getAccessByRefresh)
        {
            var token = await _tokenService.RefreshAsync(getAccessByRefresh);
            return Ok(token);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [Authorize]
        public IActionResult GetClaim() => Ok(_tokenService.GetClaims());
    }
}
