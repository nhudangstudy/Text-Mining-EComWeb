using API.Extensions;
using API.Repositories;
using Common.IRepositories;
using Common.IServices;


namespace API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISmtpService _smtpService;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, IUnitOfWork unitOfWork, ISmtpService smtpService, IHttpContextAccessor accessor)
        {
            _authenticationRepository = authenticationRepository;
            _unitOfWork = unitOfWork;
            _smtpService = smtpService;
            _accessor = accessor;
        }

        public Task<bool> IsValidAsync(CheckRequestAuthenticationModel checkRequestAuthentication)
        {
            return _authenticationRepository.IsValidAsync(checkRequestAuthentication);
        }

        private string GeneratePIN() => Random.Shared.Next(1_000_000).ToString("000000");

        public async Task SendAsync(SendRequestAuthenticationModel sendRequestAuthentication)
        {
            var isAuthExist = await _authenticationRepository.IsExistAsync(sendRequestAuthentication.Email);

            SendRequestRepoAuthenticationModel sendRequestRepo = new()
            {
                Email = sendRequestAuthentication.Email,
                Code = GeneratePIN(),
                Expire = DateTime.UtcNow.AddMinutes(5)
            };

            if (isAuthExist)
            {
                await _authenticationRepository.UpdateAsync(sendRequestRepo, sendRequestAuthentication.Email);
            }
            else
            {
                await _authenticationRepository.CreateAsync(sendRequestRepo);
            }
            await _unitOfWork.SaveChangesAsync();

            string contentMail = $"<div style=\"padding: 18px; background-color: #FFFFFF; border: 1px solid #CCCCCC; max-width: 600px; margin: 0 auto;\">\r\n    <div style=\"text-align: center;\">\r\n        <div style=\"text-align: center;\">\r\n            <img src=\"https://mcusercontent.com/5e4cb2a683e9a26c0c38cc371/images/51e28ef5-55e2-39a2-5ad7-c3e62e2fc1aa.png\" height=\"80\" style=\"width: 80px; height: 80px; border: 0;\">\r\n            <br>\r\n            <img src=\"https://mcusercontent.com/5e4cb2a683e9a26c0c38cc371/images/607e8774-2543-bc8c-0656-85f5cf49cb0c.png\" height=\"20%\" style=\"width: 200px; height: 20%; border: 0;\">\r\n        </div></div>\r\n<div style=\"text-align: left;\">\r\n<div style=\"text-align: center;\">&nbsp;</div>\r\n\r\n<h1 style=\"text-align: center;\"><span><span style=\"font-size:30px\"><strong>YÊU CẦU MÃ XÁC THỰC</strong>&nbsp;</span></span></h1>\r\n\r\n<div style=\"text-align: left;\"><br>\r\n<span style=\"font-size:16px\"><span><strong>Đây là mã&nbsp;xác thực&nbsp;của bạn!</strong></span></span></div>\r\n\r\n<p lang=\"x-size-20\" style=\"color: #222222;font-size: 14px;font-weight: normal;\"><span>Để hoàn tất đăng ký tài khoản, hãy nhập mã xác thực&nbsp;dưới đây. Mã có hạn sử dụng đến&nbsp;<strong>{sendRequestRepo.Expire.ToLocalTime()}</strong>&nbsp;<em>cho tất cả mọi yêu cầu xác thực</em>.</span></p>\r\n\r\n<p lang=\"x-size-30\" style=\"color: #222222;font-size: 14px;font-weight: normal;\">&nbsp;</p>\r\n\r\n<p lang=\"x-size-30\" style=\"text-align: center;color: #222222;font-size: 14px;font-weight: normal;\"><span><span style=\"color:#FFFFFF\"><strong><font size=\"6\"><span style=\"background-color:#FF8C00\">{sendRequestRepo.Code}</span></font></strong></span></span><br>\r\n&nbsp;</p>\r\n\r\n<p lang=\"x-size-16\" style=\"text-align: left;color: #222222;font-size: 14px;font-weight: normal;\"><span>Nếu bạn không yêu cầu mã xác thực thì ai đó đang cố gắng truy cập đến tài khoản của bạn. Hãy cẩn trọng.<br>\r\nVui lòng không cung cấp mã này cho bất kỳ ai để bảo mật thông tin cá nhân của bạn.<br>\r\n<br>\r\n<em>Đây là email tự động, vui lòng không phản hồi lại email này!</em></span></p>\r\n</div>\r\n</div>";

            await _smtpService.SendAsync(new(sendRequestAuthentication.Email), "[ITB CLUB] GỬI MÃ XÁC THỰC", contentMail);
        }
    }
}
