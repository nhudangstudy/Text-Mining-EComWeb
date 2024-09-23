
using API.Extensions;
using Common.IServices;
using Common.IRepositories;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using API.Repositories;
using AutoMapper;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IScopeRepository _scopeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _accessor;

        public AccountService(IAccountRepository accountRepository, IScopeRepository scopeRepository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticationService authenticationService, IHttpContextAccessor accessor)
        {
            _accountRepository = accountRepository;
            _scopeRepository = scopeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _accessor = accessor;
        }

        public Task<GetByIdPublicAccountModel> GetByIdPublicAsync(string id) => _accountRepository.GetByIdAsync<GetByIdPublicAccountModel>(id);


        public Task<bool> IsExistAsync(string id) => _accountRepository.IsExistAsync(id);

        public async Task<GetClaimModel> LoginAsync(LoginRequestAccountModel loginRequestAccount)
        {
            var loginAccountRepo = await _accountRepository.GetLoginInfoByIdAsync(loginRequestAccount.ClientId);

            if (IsMatchPassword(loginAccountRepo.Password, loginRequestAccount.ClientId, loginRequestAccount.Password) is false)
            {
                throw new AuthenticationException("Password is not matched.");
            }

            return await GetClaimByIdAsync(loginRequestAccount.ClientId);
        }

        private static bool IsMatchPassword(ReadOnlySpan<char> newPasswordHashed, ReadOnlySpan<char> id, ReadOnlySpan<char> password)
        {
            var valueHash = HashPassword(id, password);
            int i = 0;
            while (i < valueHash.Length
                && newPasswordHashed.Slice(i * 2, 2).Contains(valueHash[i].ToString("X2").AsSpan(), StringComparison.Ordinal))
            {
                i++;
            }

            return i == valueHash.Length;
        }

        private static byte[] HashPassword(ReadOnlySpan<char> id, ReadOnlySpan<char> password)
            => SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(password.ToString() + id.ToString()));

        private (string? First, string Last) GetName(string fullName)
        {
            fullName = fullName.Trim().ToLower();
            var firstOfSpace = fullName.IndexOf(' ');

            if (firstOfSpace is -1)
            {
                return (null, char.ToUpper(fullName[0]) + fullName[1..^0].ToLower());
            }

            var result = new StringBuilder(fullName[..(firstOfSpace + 1)]);
            result[0] = char.ToUpper(result[0]);

            for (int i = firstOfSpace + 1; i < fullName.Length; i++)
            {
                if (char.IsWhiteSpace(fullName[i - 1]) && char.IsLetter(fullName[i]))
                {
                    result.Append(char.ToUpper(fullName[i]));
                }
                else
                {
                    result.Append(fullName[i]);
                }
            }

            fullName = result.ToString();
            var lastSplit = fullName.LastIndexOf(' ');

            return (fullName[..(lastSplit)], fullName[(lastSplit + 1)..]);
        }

        public async Task RegisterAsync(RegisterRequestAccountModel registerRequestAccount)
        {
            if (registerRequestAccount.Password != registerRequestAccount.RepeatPassword)
            {
                throw new InvalidOperationException("Password is not matched.");
            }

            var registerRequestRepoAccount = _mapper.Map<RegisterRequestRepoAccountModel>(registerRequestAccount) with { RoleId = 1 , IsActive = true};

            var newPasswordHashed = HashPassword(registerRequestAccount.Email, registerRequestAccount.Password);
            var passwordBuilder = new StringBuilder(128);
            for (int i = 0; i < newPasswordHashed.Length; i++)
            {
                passwordBuilder.Append(newPasswordHashed[i].ToString("X2"));
            }
            registerRequestRepoAccount.Password = passwordBuilder.ToString();

            await _accountRepository.CreateAsync(registerRequestRepoAccount);
            await _accountRepository.AddScopeAsync(new() { Email = registerRequestRepoAccount.Email, ScopeId = 1 });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateFullNameAsync(string id, string fullName)
        {
            var updateRequestRepoAccount = await _accountRepository.GetByIdAsync<UpdateRequestRepoAccountModel>(id);
            var (First, Last) = GetName(fullName);
            updateRequestRepoAccount.FirstName = First;
            updateRequestRepoAccount.LastName = Last;

            await _accountRepository.UpdateAsync(updateRequestRepoAccount);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var updateRequestRepoAccount = await _accountRepository.GetByIdAsync<UpdateRequestRepoAccountModel>(id);
            updateRequestRepoAccount.IsActive = false;

            await _accountRepository.UpdateAsync(updateRequestRepoAccount);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<GetClaimModel> GetClaimByIdAsync(string id)
        {
            var loginAccountRepo = await _accountRepository.GetLoginInfoByIdAsync(id);
            string[]? scopes = null;

            if (loginAccountRepo.ScopeIds is not null)
            {
                scopes = await _scopeRepository.GetAllById(loginAccountRepo.ScopeIds).ToArrayAsync();
            }

            return new()
            {
                ClientId = id,
                Scopes = scopes
            };
        }

        public Task<GetActiveByIdAccountModel> GetActiveByIdAsync(string id) => _accountRepository.GetByIdAsync<GetActiveByIdAccountModel>(id);

        public async Task ActiveAsync(CheckRequestAuthenticationModel checkRequestAuthentication)
        {
            var isValidAuth = await _authenticationService.IsValidAsync(checkRequestAuthentication);

            if (isValidAuth)
            {
                var active = await GetActiveByIdAsync(checkRequestAuthentication.Email);

                if (active.IsActive is null)
                {
                    await _accountRepository.UpdateAsync<UpdateActiveRequestRepoAccountModel>
                        (new() { IsActive = true }, checkRequestAuthentication.Email);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new NotImplementedException("Can not active.");
                }
            }
        }

        public Task<GetLastNameByIdAccountModel> GetLastNameByIdAccountAsync()
        {
            var currentUserId = _accessor.HttpContext!.GetUserId();
            return _accountRepository.GetByIdAsync<GetLastNameByIdAccountModel>(currentUserId);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestAccountModel resetPasswordRequestAccount)
        {
            var checkAuth = await _authenticationService.IsValidAsync(resetPasswordRequestAccount.Auth);

            if (checkAuth)
            {
                var newPasswordHashed = HashPassword(resetPasswordRequestAccount.Auth.Email, resetPasswordRequestAccount.NewPassword);
                var newPasswordBuilder = new StringBuilder(128);
                for (int i = 0; i < newPasswordHashed.Length; i++)
                {
                    newPasswordBuilder.Append(newPasswordHashed[i].ToString("X2"));
                }
                var newPassword = newPasswordBuilder.ToString();

                await _accountRepository.UpdateAsync<ResetPasswordRequestRepoAccountModel>
                    (new() { Password = newPassword }, resetPasswordRequestAccount.Auth.Email);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("PIN is invalid.");
            }
        }
    }
}
