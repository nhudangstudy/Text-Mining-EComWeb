namespace Common.IRepositories
{
    public interface IRefreshTokenRepository : ICreateRepositoryAutoMap, IGetByIdRepositoryAutoMap<Guid>
    {
        Task<int> CountByAccountIdAsync(string accountId);
        Task DeleteLastAsync(string accountId);
    }
}