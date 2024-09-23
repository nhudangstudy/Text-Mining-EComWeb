
using API.Repositories;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext db;
        protected readonly DbSet<TEntity> entities;

        protected Repository(DbContext db)
        {
            this.db = db;
            entities = db.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity) => await db.AddAsync(entity);

        public virtual async Task DeleteAsync(params object?[]? key)
        {
            var entityToDelete = await FindAsync(key);

            if (entityToDelete is null)
            {
                throw new KeyNotFoundException();
            }

            Remove(entityToDelete);
        }

        public void Remove(TEntity entity)
        {
            if (db.Entry(entity).State is EntityState.Detached)
            {
                db.Attach(entity);
            }
            db.Remove(entity);
        }

        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<TEntity> FindAsync(params object?[]? id) => (await entities.FindAsync(id)) ?? throw new KeyNotFoundException($"Not found the {typeof(TEntity).Name}.");

        public IQueryable<TEntity> GetAll(int? take = null, int? skip = null, Expression<Func<TEntity, bool>>? filter = null, bool readOnly = true)
        {
            IQueryable<TEntity> query = entities;

            if (readOnly)
            {
                query = query.AsNoTracking();
            }
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            if (skip is not null)
            {
                query = query.Skip(skip.Value);
            }
            if (take is not null)
            {
                query = query.Take(take.Value);
            }

            return query.AsSplitQuery();
        }

        public void Update(TEntity entity)
        {
            entities.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
