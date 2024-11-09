using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IRepositories
{
    public interface ISubCategoryRepository: ICreateRepositoryAutoMap
    {
        Task<SubCategoryResponseModel?> GetByIdAsync(int id);
        Task<IEnumerable<SubCategoryResponseModel>> GetAllAsync();
        Task UpdateAsync(int id, UpdateSubCategoryModel subCategory);
        Task DeleteAsync(int id);
    }
}
