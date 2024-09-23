
using System.Linq.Expressions;

namespace Common.IRepositories
{
    public interface IFindRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object?[]? id);
    }

    public interface IFindRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity?> FindAsync(TKey id);
    }

    public interface ICheckExistRepository
    {
        Task<bool> IsExistAsync(params object?[]? id);
    }

    public interface ICheckExistRepository<TKey>
    {
        Task<bool> IsExistAsync(TKey id);
    }

    public interface IGetByIdRepository<TModel> where TModel : class
    {
        Task<TModel?> GetByIdAsync(params object?[]? id);
    }

    public interface IGetByIdRepository<TModel, TKey> where TModel : class

    {
        Task<TModel?> GetByIdAsync(TKey id);
    }

    public interface IGetAllRepository<TEntity> where TEntity : class
    {
        /// <summary>Lấy danh sách <typeparamref name="TEntity"/></summary>
        /// <param name="filter">Điều kiện lọc.</param>
        /// <inheritdoc cref="IGetAllModelRepository{TModel}.GetAllAsync(int?, int?)"/>
        IQueryable<TEntity> GetAll
            (
#pragma warning disable CS1573 // Parameter 'take' has no matching param tag in the XML comment for 'IGetAllRepository<TEntity>.GetAll(int?, int?, Expression<Func<TEntity, bool>>?, bool)' (but other parameters do)
                int? take = null,
#pragma warning restore CS1573 // Parameter 'take' has no matching param tag in the XML comment for 'IGetAllRepository<TEntity>.GetAll(int?, int?, Expression<Func<TEntity, bool>>?, bool)' (but other parameters do)
#pragma warning disable CS1573 // Parameter 'skip' has no matching param tag in the XML comment for 'IGetAllRepository<TEntity>.GetAll(int?, int?, Expression<Func<TEntity, bool>>?, bool)' (but other parameters do)
                int? skip = null,
#pragma warning restore CS1573 // Parameter 'skip' has no matching param tag in the XML comment for 'IGetAllRepository<TEntity>.GetAll(int?, int?, Expression<Func<TEntity, bool>>?, bool)' (but other parameters do)
                Expression<Func<TEntity, bool>>? filter = null,
#pragma warning disable CS1573 // Parameter 'readOnly' has no matching param tag in the XML comment for 'IGetAllRepository<TEntity>.GetAll(int?, int?, Expression<Func<TEntity, bool>>?, bool)' (but other parameters do)
                bool readOnly = true
#pragma warning restore CS1573 // Parameter 'readOnly' has no matching param tag in the XML comment for 'IGetAllRepository<TEntity>.GetAll(int?, int?, Expression<Func<TEntity, bool>>?, bool)' (but other parameters do)
            );
    }

    public interface IGetAllModelRepository<TModel>
    {
        /// <summary>Lấy danh sách <typeparamref name="TModel"/></summary>
        /// <param name="take">Số lượng phần tử cần lấy.</param>
        /// <param name="skip">Vị trí bắt đầu lấy tính từ 0.</param>   
        IAsyncEnumerable<TModel> GetAllAsync(int? take = null, int? skip = null);
    }

    public interface ICreateRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
    }

    public interface IUpdateRepository<TEntity> where TEntity : class
    {
        void Update(TEntity entity);
    }

    public interface IDeleteRepository
    {
        Task DeleteAsync(params object?[]? key);
    }

    public interface IDeleteRepository<TKey>
    {
        Task DeleteAsync(TKey key);
    }

    public interface IRepository<TEntity> :
        IFindRepository<TEntity>,
        IGetAllRepository<TEntity>,
        ICreateRepository<TEntity>,
        IUpdateRepository<TEntity>,
        IDeleteRepository
        where TEntity : class
    {

    }
}