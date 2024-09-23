namespace API.Profiles
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<CreateRequestRepoRefreshTokenModel, AppRefreshToken>();
            CreateMap<AppRefreshToken, GetByIdRefreshTokenModel>();
        }
    }
}
