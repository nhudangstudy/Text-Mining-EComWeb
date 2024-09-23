using Microsoft.EntityFrameworkCore;
using API.Entities;
using AutoMapper;

namespace API.Repositories
{
    public class AuthenticationRepository : RepositoryAutoMap<AppAuthentication>, IAuthenticationRepository
    {
        public AuthenticationRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Task<bool> IsExistAsync(string id) => entities.AnyAsync(p => p.Email == id);

        public Task<bool> IsValidAsync(CheckRequestAuthenticationModel checkRequestAuthentication)
        {
            return entities.AnyAsync(p =>
            (p.Email == checkRequestAuthentication.Email)
            && (p.Code == checkRequestAuthentication.Code)
            && (p.Expired >= DateTime.UtcNow));
        }
    }
}