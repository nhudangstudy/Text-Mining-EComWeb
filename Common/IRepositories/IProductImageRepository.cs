using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IRepositories
{
    public interface IProductImageRepository 
    {
        Task AddImagesAsync(string productAsin, List<CreateProductImageModel> imageModels);
        Task ArchiveImagesAsync(List<int> imageIds);
    }

}
