using Common.Models;

namespace Common.IServices
{
    public interface ITokenService
    {
        IEnumerable<string> GetClaims();
        Task<GetByLoginTokenModel> GetByLoginAsync(LoginRequestAccountModel loginRequestAccount, bool? includeRefreshToken);
        Task<GetByLoginTokenModel> GetByLoginAsync(string accessToken, bool? includeRefreshToken);
        Task<AccessTokenModel> RefreshAsync(GetAccessByRefreshRequestTokenModel getAccessByRefresh);
    }
}
