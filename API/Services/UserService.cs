using Autofac.Core;
using Common.IServices;


namespace API.Services
{
    public class UserService: IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UserService(IAccountRepository accountRepository, IUserRepository userRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }




        public async Task CreateNewUser(string email, CreateUpdateUserRequestModel createUpdateUserRequest)
        {
            var account = await _accountRepository.GetByIdAsync<GetByIdPublicAccountModel>(email);
            if (account == null)
            {
                throw new Exception("Your account is not valid");
            }

            var user = await _userRepository.GetByIdAsync<GetByIdUserModel>(email);
            if (user != null)
            {
                throw new Exception($"{email} have been registered.");
            }

            var userInstance = new CreateUpdateUserRequestRepoModel()
            {
                Email = email,
                DateOfBirth = createUpdateUserRequest.DateOfBirth,
                FirstName = createUpdateUserRequest.FirstName,
                LastName = createUpdateUserRequest.LastName
            };

            userInstance.Email = account.Email;
            await _userRepository.CreateAsync(userInstance);
            await _unitOfWork.SaveChangesAsync();

        }

        public Task<GetByIdUserModel?> GetUserByIdAsync(string email)
        {
            var user = _userRepository.GetUserByIdAsync(email);
            return user;
            
        }
    }
}
