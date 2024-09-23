using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _db;
        private bool _disposed = false;

        public UnitOfWork(DbContext db) => this._db = db;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task SaveChangesAsync() => _db.SaveChangesAsync();

        public Task<IDbContextTransaction> BeginTransactionAsync() => _db.Database.BeginTransactionAsync();

        public void ExecutionStrategyAsync(Action action)
        {
            _db.Database.CreateExecutionStrategy().Execute(action);
        }
    }
}
