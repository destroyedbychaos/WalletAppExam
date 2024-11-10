using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.DAL.Models.Identity;

namespace WalletApp.BLL.Services.MailService
{
    public interface IMailService
    {
        Task SendEmailAsync(string to, string subject, string text, bool isHtml = false);
        Task SendEmailAsync(IEnumerable<string> to, string subject, string message, bool isHtml = false);
        Task SendEmailAsync(MimeMessage message);
        Task SendConfirmEmailAsync(User user, string token);
    }
}
