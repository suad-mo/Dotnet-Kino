//using System.Threading.Tasks;

namespace Kino.Services.Mail
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string content);
    }
}