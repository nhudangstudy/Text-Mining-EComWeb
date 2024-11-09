using Common.IRepositories;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IServices
{
    public interface ICategoryService
    {
        Task<CategoryResponseModel?> GetByIdAsync(int id);
        Task AddAsync(CreateCategoryModel model);
        Task UpdateAsync(int id, UpdateCategoryModel model);
        Task SoftDeleteAsync(int id);
        IAsyncEnumerable<CategoryResponseModel> GetAllAsync(int size, int? page = 1);
    }
}
