using Common.Models;

namespace API.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<GetByIdPublicAccountModel, AppAccount>().ReverseMap();
            CreateMap<RegisterRequestRepoAccountModel, AppAccount>();
            CreateMap<RegisterRequestAccountModel, RegisterRequestRepoAccountModel>();
            CreateMap<AppAccount, GetActiveByIdAccountModel>();
            CreateMap<UpdateActiveRequestRepoAccountModel, AppAccount>();
            CreateMap<AppAccount, GetLastNameByIdAccountModel>();
            CreateMap<AppAccount, GetFullNameByIdAccountModel>();
            CreateMap<ResetPasswordRequestRepoAccountModel, AppAccount>();
        }
    }
}
