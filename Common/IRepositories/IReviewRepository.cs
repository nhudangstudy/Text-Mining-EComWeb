using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IRepositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ReviewResponseModel>> GetAllByProductAsinAsync(string asin);
        Task<IEnumerable<ReviewResponseModel>> GetAllByUserAsync(string email);
        Task<ReviewResponseModel?> CreateAsync(ReviewResponseModel reviewModel);
        Task<IEnumerable<ReviewResponseModel>> GetFeaturedReviewsAsync(int count, double minimumRating);
    }
}
