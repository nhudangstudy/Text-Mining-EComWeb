using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseModel>> GetAllProductsPublicAsync();
        Task<IEnumerable<ProductResponseModel>> GetAllProductsAdminAsync();
        Task<ProductResponseModel> GetProductById(string asin);
        Task UpdateProductAsync(string asin, CreateProductRequestModel model);
        Task UpdateProductPriceAsync(string asin, CreateProductPriceModel priceModel);

        IAsyncEnumerable<ProductResponseModel> GetAllProductAsync(int size, int? page = 1);
    }

}
