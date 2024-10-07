using System.Net;
using System.Net.Mail;

namespace Common.IServices
{
    public interface ISmtpService
    {
        void Login(MailAddress senderMail, NetworkCredential credentials, SmtpClient smtpClient);
        Task SendAsync(MailAddress to, string title, string content, bool isHtml = true);
    }
}