using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories
{
    public class CategoryRepository : RepositoryAutoMap<Category>, ICategoryRepository
    {

        public CategoryRepository(DbContext db, IMapper mapper) : base(db, mapper)
        {
        }
        public Task<TModel> GetByIdAsync<TModel>(int id) where TModel : class
        {
            return base.GetByIdAsync<TModel>(p => p.Id == id);
        }


        public async Task SoftDeleteAsync(int id)
        {
            var category = await db.Set<Category>().FindAsync(id);
            if (category != null)
            {
                category.IsArchived = true;
                db.Set<Category>().Update(category);
            }
        }

        public async Task UpdateAsync(int id, UpdateCategoryModel categoryModel)
        {
            var category = await db.Set<Category>().FindAsync(id);
            if (category != null)
            {
                mapper.Map(categoryModel, category);
                db.Set<Category>().Update(category);
            }
        }

    }

}
