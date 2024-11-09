using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IRepositories
{
    public interface IProductPriceHistoryRepository 
    {
        Task AddPriceHistoryAsync(string productAsin, CreateProductPriceModel priceModel);

    }

}
