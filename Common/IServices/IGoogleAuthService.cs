using Common.Models;

namespace Common.IServices
{
    public interface IGoogleAuthService
    {
        Task<string> GetEmail(string accessToken);
    }
}