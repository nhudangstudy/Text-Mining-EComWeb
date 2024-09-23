namespace API.Profiles
{
    public class AuthenticationProfile:Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<CheckRequestAuthenticationModel, AppAuthentication>();
            CreateMap<SendRequestRepoAuthenticationModel, AppAuthentication>();
        }
    }
}
