using Common.Models;

namespace Common.IServices
{
    public interface ITokenGenerateService
    {
        AccessTokenModel GenerateAccessToken(GetClaimModel getClaim);
        RefreshTokenModel GenerateRefreshToken();
    }
}