using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BrandRepository : RepositoryAutoMap<Brand>, IBrandRepository
    {

        public BrandRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
        {
            return base.GetByIdAsync<TModel>(p => p.Id == id);
        }

        public async Task SoftDeleteAsync(int id)
        {
            var brand = await db.Set<Brand>().FindAsync(id);
            if (brand != null)
            {
                brand.IsArchived = true;
                db.Set<Brand>().Update(brand);
            }
        }

        public async Task UpdateAsync(int id, UpdateBrandModel brand)
        {
            var oldBrand = await db.Set<Brand>().FindAsync(id);
            if (brand != null)
            {
                mapper.Map(brand, oldBrand);
                db.Set<Brand>().Update(oldBrand);
            }
        }

    }

}
