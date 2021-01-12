using Application.Models.Authentication;
using Application.Models.User;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> PasswordRecovery(RecoveryPasswordDto recoveryPasswordDto, UserAgentModel userAgentModel);
        Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto, UserAgentModel userAgentModel);
        Task LogoutAsync(User user, UserAgentModel userAgentModel);
        Task<ICollection<UserModel>> GetUsersByRole(long roleId);
        Task<ICollection<UserModel>> GetUsersByRoleCountry(long roleId, string idpais);
    }
}
