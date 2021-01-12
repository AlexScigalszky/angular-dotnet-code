using Application.Models.Authentication;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserLogService
    {
        Task<UserLog> AddLoginAsync(User user, UserAgentModel userAgentModel);
        Task<UserLog> AddLogoutAsync(User user, UserAgentModel userAgentModel);
        Task<UserLog> AddPasswordRecoveryAsync(User user, UserAgentModel userAgentModel);
        Task<UserLog> AddForgotPasswordAsync(User user, UserAgentModel userAgentModel);
    }
}