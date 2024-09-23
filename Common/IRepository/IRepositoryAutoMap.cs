using System.Linq.Expressions;

namespace Common.IRepositories
{
    public interface IGetAllRepositoryAutoMap
    {
        IAsyncEnumerable<TModel> GetAllAsync<TModel>(int? take = null, int? skip = null, Expression<Func<TModel, bool>>? condition = null, bool readOnly = true) where TModel : class;
    }

    public interface IGetByIdRepositoryAutoMap<TKey>
    {
        Task<TModel> GetByIdAsync<TModel>(TKey id) where TModel : class;
    }

    public interface ICreateRepositoryAutoMap
    {
        public Task CreateAsync<TModel>(TModel model) where TModel : class;

        Task<TResponse> CreateAsync<TResponse, TModel>(TModel model, Func<Task> saveChangeAsync) where TModel : class
            where TResponse : class;
    }

    public interface IUpdateRepositoryAutoMap
    {
        Task UpdateAsync<TModel>(TModel model, params object?[]? id) where TModel : class;
    }

    public interface IUpdateRepositoryAutoMap<TKey>
    {
        Task UpdateAsync<TModel>(TKey id, TModel model) where TModel : class;
    }

    public interface IDeleteRepositoryAutoMap
    {
        Task DeleteAsync<TModel>(TModel model) where TModel : class;
    }

    public interface IRepositoryAutoMap : IGetAllRepositoryAutoMap, ICreateRepositoryAutoMap, IUpdateRepositoryAutoMap
    {
    }
}