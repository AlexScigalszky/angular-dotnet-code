using Application.Interfaces;
using Application.Models.Authentication;
using Domain.Constants;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserLogService : IUserLogService
    {
        private const string INDICE_IDUser = "IDUser";
        private readonly IUserLogRepository _userLogRepository;

        public UserLogService(IUserLogRepository userLogRepository)
        {
            _userLogRepository = userLogRepository ?? throw new ArgumentNullException(nameof(userLogRepository));
        }

        public Task<UserLog> AddForgotPasswordAsync(User user, UserAgentModel userAgentModel)
        {
            var log = new UserLog()
            {
                Date = DateTime.UtcNow,
                UserId = user.Id,
                Notes = userAgentModel.Data,
                User = user,
                Action = UserLogConstants.FORGOT_PASSWORD,
            };
            return _userLogRepository.AddAsync(log);
        }

        public Task<UserLog> AddLoginAsync(User user, UserAgentModel userAgentModel)
        {
            var log = new UserLog()
            {
                Date = DateTime.UtcNow,
                UserId = user.Id,
                Notes = userAgentModel.Data,
                User = user,
                Action = UserLogConstants.LOGIN,
            };
            return _userLogRepository.AddAsync(log);
        }

        public Task<UserLog> AddLogoutAsync(User user, UserAgentModel userAgentModel)
        {
            var log = new UserLog()
            {
                Date = DateTime.UtcNow,
                UserId = user.Id,
                Notes = userAgentModel.Data,
                User = user,
                Action = UserLogConstants.LOGOUT,
            };
            return _userLogRepository.AddAsync(log);
        }

        public Task<UserLog> AddPasswordRecoveryAsync(User user, UserAgentModel userAgentModel)
        {
            var log = new UserLog()
            {
                Date = DateTime.UtcNow,
                UserId = user.Id,
                Notes = userAgentModel.Data,
                User = user,
                Action = UserLogConstants.PASSWORD_RECOVERY,
            };
            return _userLogRepository.AddAsync(log);
        }
    }
}
