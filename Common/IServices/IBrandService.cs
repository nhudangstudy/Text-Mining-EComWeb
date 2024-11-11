using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IServices
{
    public interface IBrandService
    {
        Task<BrandResponseModel?> GetByIdAsync(int id);
        IAsyncEnumerable<BrandResponseModel> GetAllAsync(int size, int? page = 1);
        Task AddAsync(CreateBrandModel model);
        Task UpdateAsync(int id, UpdateBrandModel model);
        Task SoftDeleteAsync(int id);
    }

}
