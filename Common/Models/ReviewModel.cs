using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ReviewResponseModel
    {
        public Guid Id { get; set; }
        public string Asin { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public double StarRating { get; set; }
        public string ReviewContent { get; set; } = null!;
        public string OwnedBy { get; set; } = null!;
    }

    public class CreateReviewRequest
    {
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public double StarRating { get; set; }
        public string ReviewContent { get; set; } = null!;
    }

}
