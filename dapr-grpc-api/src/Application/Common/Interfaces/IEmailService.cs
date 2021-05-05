using System.Net.Mail;
using System.Threading.Tasks;

namespace SC.API.CleanArchitecture.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAsync(MailMessage emailMessage);
    }
}
