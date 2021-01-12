using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string from, string nameTo, string to, string subject, string message);
    }
}
