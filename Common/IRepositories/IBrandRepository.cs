using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IRepositories
{
    public interface IBrandRepository : IRepositoryAutoMap, IGetByIdRepositoryAutoMap<int>, IGetAllRepositoryAutoMap
    {
        Task SoftDeleteAsync(int id);
        Task UpdateAsync(int id, UpdateBrandModel brand);
    }

}
