using AutoMapper.QueryableExtensions;
using AutoMapper;
using Common.IRepositories;
using Common.IServices;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace API.Repositories
{
    public abstract class RepositoryAutoMap<TEntity> : Repository<TEntity>, IRepositoryAutoMap where TEntity : class
    {
        protected readonly IMapper mapper;

        public RepositoryAutoMap(DbContext db, IMapper mapper) : base(db) => this.mapper = mapper;

        public async Task CreateAsync<TModel>(TModel model) where TModel : class
        {
            var entity = mapper.Map<TEntity>(model);
            await base.CreateAsync(entity);
        }

        public async Task<TResponse> CreateAsync<TResponse, TModel>(TModel model, Func<Task> saveChangeAsync) where TModel : class
            where TResponse : class
        {
            var entity = mapper.Map<TEntity>(model);
            await base.CreateAsync(entity);

            await saveChangeAsync();

            return mapper.Map<TResponse>(entity);
        }

        public IAsyncEnumerable<TModel> GetAllAsync<TModel>(int? take = null, int? skip = null, Expression<Func<TModel, bool>>? condition = null, bool readOnly = true) where TModel : class
        {
            var filter = mapper.Map<Expression<Func<TEntity, bool>>>(condition);

            return GetAll(take, skip, filter, readOnly)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .AsAsyncEnumerable();
        }

        public async Task UpdateAsync<TModel>(TModel model, params object?[]? id) where TModel : class
        {
            var oldData = await FindAsync(id);
            var newData = mapper.Map(model, oldData);

            Update(newData);
        }

        protected async Task<TModel> GetByIdAsync<TModel>(Expression<Func<TEntity, bool>> idFilter) where TModel : class
        {
            var data = await entities
                .Where(idFilter)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (data is null)
            {
                throw new KeyNotFoundException($"Not found {typeof(TEntity).Name}");
            }
            return data;
        }

        protected void Update<TModel>(TModel model) where TModel : class
        {
            var entity = mapper.Map<TEntity>(model);
            base.Update(entity);
        }
    }
}