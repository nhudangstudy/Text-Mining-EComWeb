namespace API.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponseModel>().ReverseMap();
            CreateMap<CreateReviewRequest, Review>();
            CreateMap<CreateReviewRequest, ReviewResponseModel>();
        }
    }
}
