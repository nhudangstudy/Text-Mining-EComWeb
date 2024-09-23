using Common.Models;

namespace Common.IRepositories
{
    public interface IAuthenticationRepository : ICreateRepositoryAutoMap, ICheckExistRepository<string>, IUpdateRepositoryAutoMap
    {
        Task<bool> IsValidAsync(CheckRequestAuthenticationModel checkRequestAuthentication);
    }
}