
using Common.Models;

namespace Common.IRepositories
{
    public interface IAccountRepository : IRepositoryAutoMap, ICheckExistRepository<string>, IGetByIdRepositoryAutoMap<string>
    {
        Task<LoginAccountRepoModel> GetLoginInfoByIdAsync(string id);
        Task AddScopeAsync(AddScopeRequestRepoAccountModel addScopeRequestRepoAccount);
        Task RemoveScopeAsync(RemoveScopeRequestRepoAccountModel removeScopeRequestRepoAccount);
    }
}