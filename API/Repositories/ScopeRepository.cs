
using API.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Repositories
{
    public class ScopeRepository : RepositoryAutoMap<AppScope>, IScopeRepository
    {
        public ScopeRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public IAsyncEnumerable<string> GetAllById(IEnumerable<int> ids) => GetAll(filter: p => ids.Contains(p.Id))
                .Select(p => p.Value)
                .AsAsyncEnumerable();
    }
}
