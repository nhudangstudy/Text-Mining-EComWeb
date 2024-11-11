using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SubCategoryRepository : RepositoryAutoMap<SubCategory>, ISubCategoryRepository
    {

        public SubCategoryRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }

        public async Task<SubCategoryResponseModel?> GetByIdAsync(int id)
        {
            var subCategory = await db.Set<SubCategory>()
                .Include(sc => sc.Category)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            return mapper.Map<SubCategoryResponseModel>(subCategory);
        }

        public async Task<IEnumerable<SubCategoryResponseModel>> GetAllAsync()
        {
            var subCategories = await db.Set<SubCategory>()
                .Include(sc => sc.Category)
                .ToListAsync();

            return mapper.Map<IEnumerable<SubCategoryResponseModel>>(subCategories);
        }

        public async Task AddAsync(CreateSubCategoryModel subCategoryModel)
        {
            var subCategory = mapper.Map<SubCategory>(subCategoryModel);
            await db.Set<SubCategory>().AddAsync(subCategory);
        }

        public async Task UpdateAsync(int id, UpdateSubCategoryModel subCategoryModel)
        {
            var subCategory = await db.Set<SubCategory>().FindAsync(id);
            if (subCategory != null)
            {
                mapper.Map(subCategoryModel, subCategory);
                db.Set<SubCategory>().Update(subCategory);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var subCategory = await db.Set<SubCategory>().FindAsync(id);
            if (subCategory != null)
            {
                subCategory.IsArchived = true;
                db.Set<SubCategory>().Update(subCategory);
            }
        }

    }

}
