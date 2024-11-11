namespace API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponseModel>();
            CreateMap<CreateCategoryModel, Category>();
            CreateMap<UpdateCategoryModel, Category>();
            CreateMap<Category, Category>();
            CreateMap<Category, SubCategoryResponseModel>();
        }
    }

}
