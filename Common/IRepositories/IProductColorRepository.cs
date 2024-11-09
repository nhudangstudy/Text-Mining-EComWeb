using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IRepositories
{
    public interface IProductColorRepository
    {
        Task AddColorsAsync(string productAsin, List<CreateProductColorModel> colorModels);
    }

}
