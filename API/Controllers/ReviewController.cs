using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using API.Services;
namespace API.Controllers


{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("product/{asin}")]
        public async Task<ActionResult<IEnumerable<ReviewResponseModel>>> GetAllByProductAsin(string asin)
        {
            if (string.IsNullOrEmpty(asin))
            {
                return BadRequest("Product ASIN is required.");
            }

            var reviews = await _reviewService.GetAllCommentsByProductAsinAsync(asin);
            if (reviews == null || !reviews.Any())
            {
                return NotFound("No reviews found for the given ASIN.");
            }

            var response = reviews.Select(r => new ReviewResponseModel
            {
                Id = r.Id,
                Asin = r.Asin,
                Title = r.Title,
                ImageUrl = r.ImageUrl,
                StarRating = r.StarRating,
                ReviewContent = r.ReviewContent,
                OwnedBy = r.OwnedBy // This should already be the full name if handled in the service
            });

            return Ok(response);
        }

        [HttpGet("user/{email}")]
        public async Task<ActionResult<IEnumerable<ReviewResponseModel>>> GetAllByUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("User email is required.");
            }

            var reviews = await _reviewService.GetAllCommentsByUserAsync(email);
            if (reviews == null || !reviews.Any())
            {
                return NotFound("No reviews found for the given user.");
            }

            var response = reviews.Select(r => new ReviewResponseModel
            {
                Id = r.Id,
                Asin = r.Asin,
                Title = r.Title,
                ImageUrl = r.ImageUrl,
                StarRating = r.StarRating,
                ReviewContent = r.ReviewContent,
                OwnedBy = r.OwnedBy
            });

            return Ok(response);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<IEnumerable<ReviewResponseModel>>> GetFeaturedReviews([FromQuery] int count = 4, [FromQuery] double minimumRating = 4.0)
        {
            var reviews = await _reviewService.GetFeaturedReviewsAsync(count, minimumRating);
            if (reviews == null || !reviews.Any())
            {
                return NotFound("No featured reviews found.");
            }

            var response = reviews.Select(r => new ReviewResponseModel
            {
                Id = r.Id,
                Asin = r.Asin,
                Title = r.Title,
                ImageUrl = r.ImageUrl,
                StarRating = r.StarRating,
                ReviewContent = r.ReviewContent,
                OwnedBy = r.OwnedBy,
                Sentiment = r.Sentiment
            });

            return Ok(response);
        }

        [HttpPost("{asin}")]
        public async Task<IActionResult> Create(string asin, [FromBody] CreateReviewRequest reviewRequest)
        {
            if (reviewRequest == null)
            {
                return BadRequest("Review content is required.");
            }

            var createdReview = await _reviewService.CreateCommentAsync(asin, reviewRequest);
            if (createdReview == null)
            {
                return BadRequest("Failed to create the review.");
            }

            return CreatedAtAction(nameof(Create), new { id = createdReview.Id }, createdReview);
        }
    }
}
