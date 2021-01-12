using Application.Models.Authentication;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationModel> Authenticate(AuthenticationDto authenticationDto, UserAgentModel userAgentData);
        Task<AuthenticationModel> GetAuthenticatedUser(User user);
        Task<AuthenticationModel> RefreshToken(string token);
        Task<User> FetchUser(string token);
        byte[] Encript(string password);
    }
}
