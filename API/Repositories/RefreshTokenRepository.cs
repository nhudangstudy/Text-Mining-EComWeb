using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RefreshTokenRepository : RepositoryAutoMap<AppRefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Task<int> CountByAccountIdAsync(string accountId) => entities.CountAsync(p => p.AccountId == accountId);

        public async Task DeleteLastAsync(string accountId)
        {
            var lastItem = await entities
                .Where(p => p.AccountId == accountId)
                .OrderByDescending(p => p.ExpiredTime) // Replace with the appropriate field to sort by
                .LastOrDefaultAsync();

            if (lastItem is null)
            {
                return;
            }

            Remove(lastItem);
        }

        public Task<TModel> GetByIdAsync<TModel>(Guid id) where TModel : class => base.GetByIdAsync<TModel>(p => p.RefreshTokenId == id);
    }
}
