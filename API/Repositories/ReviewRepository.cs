using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public ReviewRepository(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetAllByProductAsinAsync(string asin)
        {
            var reviews = await _dbContext.Set<Review>().Where(r => r.Asin == asin).ToListAsync();
            return _mapper.Map<IEnumerable<ReviewResponseModel>>(reviews);
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetAllByUserAsync(string email)
        {
            var reviews = await _dbContext.Set<Review>().Where(r => r.OwnedBy == email).ToListAsync();
            return _mapper.Map<IEnumerable<ReviewResponseModel>>(reviews);
        }

        public async Task<ReviewResponseModel?> CreateAsync(ReviewResponseModel reviewModel)
        {
            var reviewEntity = _mapper.Map<Review>(reviewModel);
            _dbContext.Set<Review>().Add(reviewEntity);
            return _mapper.Map<ReviewResponseModel>(reviewEntity);
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetFeaturedReviewsAsync(int count, double minimumRating)
        {
            var highRatedReviews = await _dbContext.Set<Review>()
                .Where(r => r.StarRating >= minimumRating)
                .OrderBy(r => Guid.NewGuid()) // Randomize the order
                .Take(count)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ReviewResponseModel>>(highRatedReviews);
        }
    }
}
