using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IServices
{
    public interface ISubCategoryService
    {
        Task<SubCategoryResponseModel?> GetByIdAsync(int id);
        Task<IEnumerable<SubCategoryResponseModel>> GetAllAsync();
        Task CreateAsync(CreateSubCategoryModel model);
        Task UpdateAsync(int id, UpdateSubCategoryModel model);
        Task DeleteAsync(int id);
    }

}
