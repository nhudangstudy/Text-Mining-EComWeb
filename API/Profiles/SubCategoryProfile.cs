using API.Entities;
using AutoMapper;
using Common.Models;


namespace API.Profiles
{
    public class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategory, SubCategoryResponseModel>();
            CreateMap<CreateSubCategoryModel, SubCategory>();
            CreateMap<UpdateSubCategoryModel, SubCategory>();
        }
    }

}
