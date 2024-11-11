namespace API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Existing mappings
            CreateMap<CreateUpdateUserRequestRepoModel, CreateUpdateUserRequestModel>();
            CreateMap<CreateUpdateUserRequestModel, CreateUpdateUserRequestRepoModel>();
            CreateMap<GetByIdUserModel, User>();
            CreateMap<User, GetByIdUserModel>();

            // Mapping for CreateUpdateUserRequestRepoModel to API.Entities.User with DateTime -> DateOnly conversion
            CreateMap<CreateUpdateUserRequestRepoModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth))) // Convert DateTime to DateOnly
                .ForMember(dest => dest.IsArchived, opt => opt.Ignore());  // Optionally map or ignore other properties

            CreateMap<User, CreateUpdateUserResponeModel>()
               .ForMember(dest => dest.Message, opt => opt.MapFrom(src => "User operation successful"));
        }
    }
}
