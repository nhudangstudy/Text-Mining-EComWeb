using Microsoft.EntityFrameworkCore.Storage;

namespace Common.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        void ExecutionStrategyAsync(Action action);
        Task SaveChangesAsync();
    }
}
