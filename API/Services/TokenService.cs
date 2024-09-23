using Microsoft.Identity.Client;

using System.Security.Authentication;
using System.Security.Claims;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenGenerateService _tokenGenerateService;
        private readonly IAccountService _accountService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;
        private readonly IGoogleAuthService _googleAuthService;

        public TokenService(ITokenGenerateService tokenGenerateService, IAccountService accountService, IHttpContextAccessor accessor, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, IGoogleAuthService googleAuthService)
        {
            _tokenGenerateService = tokenGenerateService;
            _accountService = accountService;
            _accessor = accessor;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _googleAuthService = googleAuthService;
        }

        public async Task<GetByLoginTokenModel> GetByLoginAsync(LoginRequestAccountModel loginRequestAccount, bool? includeRefreshToken)
        {
            var accountActive = await _accountService.GetActiveByIdAsync(loginRequestAccount.ClientId);

            switch (accountActive.IsActive)
            {
                case true:
                    var claims = await _accountService.LoginAsync(loginRequestAccount);
                    return await GetByClaimsAsync(claims, includeRefreshToken);
                case false:
                    throw new AuthenticationException("The account was disabled.");
                case null:
                    throw new AuthenticationException("The account has not been activated.");
            }
        }

        private async Task<GetByLoginTokenModel> GetByClaimsAsync(GetClaimModel claims, bool? includeRefreshToken)
        {
            var accessToken = _tokenGenerateService.GenerateAccessToken(claims!);

            if (includeRefreshToken is true)
            {
                var countRefreshToken = await _refreshTokenRepository.CountByAccountIdAsync(claims.ClientId);
                if (countRefreshToken > 5)
                {
                    await _refreshTokenRepository.DeleteLastAsync(claims.ClientId);
                }

                var refreshToken = _tokenGenerateService.GenerateRefreshToken();
                CreateRequestRepoRefreshTokenModel createRequestRepoRefreshToken = new()
                {
                    AccountId = claims.ClientId,
                    ExpiredTime = refreshToken.Expire,
                    RefreshTokenId = refreshToken.Value,
                    Ipaddress = _accessor.HttpContext!.Connection?.RemoteIpAddress?.ToString() ?? "Unknown",
                };
                await _refreshTokenRepository.CreateAsync(createRequestRepoRefreshToken);

                await _unitOfWork.SaveChangesAsync();

                return new()
                {
                    Access = accessToken,
                    Refresh = refreshToken
                };
            }

            return new() { Access = accessToken };
        }

        public async Task<GetByLoginTokenModel> GetByLoginAsync(string accessToken, bool? includeRefreshToken)
        {
            var accountId = await _googleAuthService.GetEmail(accessToken);
            var accountActive = await _accountService.GetActiveByIdAsync(accountId);

            switch (accountActive.IsActive)
            {
                case true:
                    var claims = await _accountService.GetClaimByIdAsync(accountId);
                    return await GetByClaimsAsync(claims, includeRefreshToken);
                case false:
                    throw new AuthenticationException("The account was disabled.");
                case null:
                    throw new AuthenticationException("The account has not been activated.");
            }

            throw new AuthenticationException();
        }

        public IEnumerable<string> GetClaims() => _accessor.HttpContext!.User.Claims.Select(p => p.Value);

        public async Task<AccessTokenModel> RefreshAsync(GetAccessByRefreshRequestTokenModel getAccessByRefresh)
        {
            var refreshToken = await _refreshTokenRepository.GetByIdAsync<GetByIdRefreshTokenModel>(getAccessByRefresh.RefreshToken);

            if (refreshToken.ExpiredTime < DateTime.UtcNow)
            {
                throw new TimeoutException("The refresh token has expired.");
            }

            var claims = await _accountService.GetClaimByIdAsync(refreshToken.AccountId);

            return _tokenGenerateService.GenerateAccessToken(claims);
        }
    }
}
