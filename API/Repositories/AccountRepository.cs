
using API.Repositories;
using AutoMapper;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITBClub.API.Repositories
{
    public class AccountRepository : RepositoryAutoMap<AppAccount>, IAccountRepository
    {
        public AccountRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task AddScopeAsync(AddScopeRequestRepoAccountModel addScopeRequestRepoAccount)
        {
            var account = await FindAsync(addScopeRequestRepoAccount.Email);

            var scope = await db.Set<AppScope>().FindAsync(addScopeRequestRepoAccount.ScopeId);
            account.Scopes.Add(scope);
        }

        public async Task RemoveScopeAsync(RemoveScopeRequestRepoAccountModel removeScopeRequestRepoAccount)
        {
            var account = await FindAsync(removeScopeRequestRepoAccount.Email);

            var scope = await db.Entry(account)
                .Collection(p => p.Scopes)
                .Query()
                .FirstAsync(p => p.Id == removeScopeRequestRepoAccount.ScopeId);

            account.Scopes.Remove(scope);
        }

        public Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
            => GetByIdAsync<TModel>(p => p.Email == id);

        public async Task<LoginAccountRepoModel> GetLoginInfoByIdAsync(string id)
        {
            var account = await FindAsync(id);

            var scopes = db.Entry(account)
                .Collection(p => p.Scopes)
                .Query()
                .Select(p => p.Id)
                .AsEnumerable();

            return new()
            {
                Password = account.Password,
                ScopeIds = scopes
            };
        }

        public Task<bool> IsExistAsync(string id)
            => entities.AnyAsync(p => p.Email == id);
    }
}