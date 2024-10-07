using Common.Models;

namespace Common.IServices
{
    public interface IAccountService
    {
        Task<GetLastNameByIdAccountModel> GetLastNameByIdAccountAsync();
        Task<GetActiveByIdAccountModel> GetActiveByIdAsync(string id);
        Task<GetByIdPublicAccountModel> GetByIdPublicAsync(string id);
        Task ActiveAsync(CheckRequestAuthenticationModel checkRequestAuthentication);
        Task ResetPasswordAsync(ResetPasswordRequestAccountModel resetPasswordRequestAccount);
        Task<GetClaimModel> LoginAsync(LoginRequestAccountModel loginRequestAccount);
        Task<GetClaimModel> GetClaimByIdAsync(string id);
        Task RegisterAsync(RegisterRequestAccountModel registerRequestAccount);
        Task UpdateFullNameAsync(string id, string fullName);
        Task DeleteAsync(string id);
        Task<bool> IsExistAsync(string id);
    }
}