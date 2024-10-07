using AutoMapper.QueryableExtensions;
using AutoMapper;
using Common.IRepositories;
using Common.IServices;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace API.Repositories
{
    public class UserRepository: RepositoryAutoMap<User>, IUserRepository
    {
        public UserRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Task<TModel> GetByIdAsync<TModel>(string id) where TModel : class
            => GetByIdAsync<TModel>(p => p.Email == id);

    }
}
