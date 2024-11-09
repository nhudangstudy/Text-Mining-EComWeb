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
        public async Task CreateNewUser(string email, CreateUpdateUserRequestModel model)
        {
            // Map request model to repository model
            var userEntity = mapper.Map<User>(model);
            userEntity.Email = email;

            await db.Set<User>().AddAsync(userEntity);
            await db.SaveChangesAsync();
        }


        public async Task<GetByIdUserModel?> GetUserByIdAsync(string email)
        {
            var user = await db.Set<User>()
                .Where(u => u.Email == email)
                .ProjectTo<GetByIdUserModel>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
