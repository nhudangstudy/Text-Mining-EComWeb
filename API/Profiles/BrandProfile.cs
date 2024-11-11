namespace API.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandResponseModel>();
            CreateMap<CreateBrandModel, Brand>();
            CreateMap<UpdateBrandModel, Brand>();
        }
    }

}
