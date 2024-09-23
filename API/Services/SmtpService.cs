
using System.Net;
using System.Net.Mail;
using Common.IRepositories;
using Common.IServices;

namespace API.Services
{
    public class SmtpService : ISmtpService
    {
        private MailAddress _senderMail;
        private SmtpClient _smtpClient;

        public SmtpService(Config config)
        {
            var info = config.SmtpNoreply;
            Login(new(info.Email, info.DisplayName), new(info.Email, info.Password), new(info.Host, info.Port));
        }

        public void Login(MailAddress senderMail, NetworkCredential credentials, SmtpClient smtpClient)
        {
            _senderMail = senderMail;

            _smtpClient = smtpClient;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = credentials;
            _smtpClient.EnableSsl = true;
        }

        public async Task SendAsync(MailAddress to, string title, string content, bool isHtml = true)
        {
            try
            {
                using (var client = _smtpClient)
                {
                    using (var message = new MailMessage())
                    {
                        message.From = _senderMail;
                        message.To.Clear();
                        message.To.Add(to);
                        message.Subject = title;

                        if (isHtml)
                        {
                            content += "<div style=\"text-align: left;\"><span style=\"font-size:14px\"><span style=\"color:#FF8C00\"><strong>ITB Club</strong>&nbsp;</span>/&nbsp;Khoa Hệ thống thông tin<br>\r\n    <a href=\"mailto:itbclub@st.uel.edu.vn\" target=\"_blank\">itbclub@st.uel.edu.vn</a><br>\r\n    <strong>Trường Đại học Kinh tế - Luật ( Đại học Quốc gia TP Hồ Chí Minh )</strong><br>\r\n    Văn phòng Đoàn Thanh Niên, Trường Đại Học Kinh tế - Luậ­t, Khu phố 3, P. Linh Trung, Q.Thủ Đức.</span></div></div>";
                        }
                        message.Body = content;

                        message.IsBodyHtml = isHtml;

                        await client.SendMailAsync(message);
                    }
                }
            }
            catch { throw; }
        }
    }
}