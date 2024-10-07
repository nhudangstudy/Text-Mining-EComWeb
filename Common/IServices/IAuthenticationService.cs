using Common.Models;

namespace Common.IServices
{
    public interface IAuthenticationService
    {
        Task SendAsync(SendRequestAuthenticationModel sendRequestAuthentication);
        Task<bool> IsValidAsync(CheckRequestAuthenticationModel checkRequestAuthentication);
    }
}