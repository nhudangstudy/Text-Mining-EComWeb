using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IServices
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseModel>> GetAllCommentsByProductAsinAsync(string asin);
        Task<IEnumerable<ReviewResponseModel>> GetAllCommentsByUserAsync(string email);
        Task<ReviewResponseModel?> CreateCommentAsync(string asin, CreateReviewRequest reviewRequest);

        Task<IEnumerable<ReviewResponseModel>> GetFeaturedReviewsAsync(int count, double minimumRating);
    }
}
