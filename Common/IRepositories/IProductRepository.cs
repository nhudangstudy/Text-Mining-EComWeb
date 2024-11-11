using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.IRepositories
{
    public interface IProductRepository :IGetByIdRepositoryAutoMap<string>, IGetAllRepositoryAutoMap
    {
        Task AddWithDetailsAsync(CreateProductModel model);
        Task UpdateAsync(CreateProductRequestModel product);

    }

}
