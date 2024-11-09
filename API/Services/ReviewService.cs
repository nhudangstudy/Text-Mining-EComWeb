using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repositories;
using Common.IRepositories;
using API.Extensions;
using AutoMapper;

namespace API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;

        public ReviewService(IReviewRepository reviewRepository, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accessor = accessor;
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetAllCommentsByProductAsinAsync(string asin)
        {
            var reviews = await _reviewRepository.GetAllByProductAsinAsync(asin);
            return await AddReviewerFullNameToReviews(reviews);
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetAllCommentsByUserAsync(string email)
        {
            var reviews = await _reviewRepository.GetAllByUserAsync(email);
            return await AddReviewerFullNameToReviews(reviews);
        }

        public async Task<ReviewResponseModel?> CreateCommentAsync( string asin, CreateReviewRequest reviewRequest)
        {
            var email = _accessor.HttpContext!.GetUserId();
            var reviewModel = _mapper.Map<ReviewResponseModel>(reviewRequest);
            reviewModel.Asin = asin;
            reviewModel.OwnedBy = email;
            var createdReview = await _reviewRepository.CreateAsync(reviewModel);

            if (createdReview != null)
            {
                var user = await _userRepository.GetUserByIdAsync(email);
                if (user != null)
                {
                    createdReview.OwnedBy = $"{user.FirstName} {user.LastName}";
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return createdReview;
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetFeaturedReviewsAsync(int count, double minimumRating)
        {
            var reviews = await _reviewRepository.GetFeaturedReviewsAsync(count, minimumRating);
            return await AddReviewerFullNameToReviews(reviews);
        }

        private async Task<IEnumerable<ReviewResponseModel>> AddReviewerFullNameToReviews(IEnumerable<ReviewResponseModel> reviews)
        {
            var updatedReviews = new List<ReviewResponseModel>();

            foreach (var review in reviews)
            {
                var user = await _userRepository.GetUserByIdAsync(review.OwnedBy);
                if (user != null)
                {
                    review.OwnedBy = $"{user.FirstName} {user.LastName}";
                }
                updatedReviews.Add(review);
            }

            return updatedReviews;
        }
    }
}
