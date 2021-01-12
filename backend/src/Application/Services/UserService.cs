using Application.Interfaces;
using Application.Mapper;
using Application.Models.Authentication;
using Application.Models.User;
using Core.Configuration;
using Core.Interfaces;
using Core.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Repositories;
using Domain.Specifications;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private const string KEYS_NOT_MATCH = "Las claves no coinciden";
        private const string TEMPLATE_NOT_FOUND = "Plantilla no encontrada";
        private const string USER_NOT_FOUND = "User no encontrado";
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly IOptions<Settings> _options;
        private readonly IEmailSender _emailSender;
        private readonly IUserLogService _userLogService;
        private readonly IAppLogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
            IAuthenticationService authenticationService,
            IMailTemplateRepository mailTemplateRepository,
            IOptions<Settings> options,
            IEmailSender emailSender,
            IUserLogService userLogService,
            IAppLogger<UserService> logger
            )
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _mailTemplateRepository = mailTemplateRepository ?? throw new ArgumentNullException(nameof(mailTemplateRepository));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _userLogService = userLogService ?? throw new ArgumentNullException(nameof(userLogService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> ForgotPassword(ForgotPasswordDto forgotPasswordDto, UserAgentModel userAgentModel)
        {
            var user = await _userRepository.GetByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                throw new Exceptions.ApplicationException(USER_NOT_FOUND);
            }
            var newPassword = RandomGenerator.NewRandomPassword();
            user.Password = _authenticationService.Encript(newPassword);
            await _userRepository.UpdateAsync(user);

            var templateEntity = await _mailTemplateRepository.GetByIdAsync(MailConstants.ID_RECOVERY_PASSWORD);
            if (string.IsNullOrWhiteSpace(templateEntity.Template))
            {
                _logger.LogInformation("Template not found", templateEntity.Template);
                throw new Exceptions.ApplicationException(TEMPLATE_NOT_FOUND);
            }

            await _userLogService.AddForgotPasswordAsync(user, userAgentModel);

            var redirectLink = _options.Value.LinksOptions.ResetPasswordLink + $"{newPassword}";

            var body = templateEntity.Template
                .Replace(MailConstants.KEY_USER_MAIL, user.Username)
                .Replace(MailConstants.KEY_LINK_TO_REDIRECT, redirectLink);

            return await _emailSender.SendEmailAsync("SEAPOL", user.Username, user.Username, templateEntity.Subject, body);
        }

        public async Task<bool> PasswordRecovery(RecoveryPasswordDto recoveryPasswordDto, UserAgentModel userAgentModel)
        {
            var user = await _userRepository.GetByEmailAsync(recoveryPasswordDto.Email);

            var hashPassword = _authenticationService.Encript(recoveryPasswordDto.Secret);
            if (!user.HasPassword(hashPassword))
            {
                _logger.LogInformation("Password Recovery Fail", recoveryPasswordDto.Email);
                throw new Exceptions.ApplicationException(KEYS_NOT_MATCH);
            }

            user.Password = _authenticationService.Encript(recoveryPasswordDto.Password);
            await _userRepository.UpdateAsync(user);

            await _userLogService.AddPasswordRecoveryAsync(user, userAgentModel);

            var templateEntity = await _mailTemplateRepository.GetByIdAsync(MailConstants.ID_CREDENTIALS);
            if (string.IsNullOrWhiteSpace(templateEntity.Template))
            {
                throw new Exceptions.ApplicationException(TEMPLATE_NOT_FOUND);
            }

            var body = templateEntity.Template
                .Replace(MailConstants.KEY_USER_MAIL, user.Username)
                .Replace(MailConstants.KEY_PASSWORD, recoveryPasswordDto.Password);

            return await _emailSender.SendEmailAsync("SEAPOL", user.Username, user.Username, templateEntity.Subject, body);
        }

        public Task LogoutAsync(User user, UserAgentModel userAgentModel)
        {
            return _userLogService.AddLogoutAsync(user, userAgentModel);
        }

        public async Task<ICollection<UserModel>> GetUsersByRole(long roleId)
        {
            var spec = new UserWithRoleSpecification(id: null, roleId: roleId);
            var userList = await _userRepository.GetAsync(spec);
            var mapped = ObjectMapper.Mapper.Map<ICollection<UserModel>>(userList);
            return mapped;
        }

        public async Task<ICollection<UserModel>> GetUsersByRoleCountry(long roleId, string countryId)
        {
            var spec = new UserWithRoleSpecification(roleId, countryId);
            var userList = await _userRepository.GetAsync(spec);
            var mapped = ObjectMapper.Mapper.Map<ICollection<UserModel>>(userList);
            return mapped;
        }
    }
}
