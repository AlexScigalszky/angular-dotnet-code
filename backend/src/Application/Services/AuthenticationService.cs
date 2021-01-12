using Application.Exceptions;
using Application.Interfaces;
using Application.Models.Authentication;
using Core.Configuration;
using Core.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string INVALID_LOGIN = "Invalid email or password";
        private const string INVALID_TOKEN = "Invalid token";
        private const string EXCEED_LOGIN_ATTEMPTS = "Exceeded the limit of login attempts";
        private const int ID_SUPPORT_USER = 99;
        private readonly AuthenticationOptions _options;
        private readonly IUserRepository _userRepository;
        private readonly IUserLogService _userLogService;
        private readonly MD5CryptoServiceProvider _cryptoServiceProvider;
        private readonly IAppLogger<AuthenticationService> _logger;

        public AuthenticationService(IOptions<Settings> options,
            IUserRepository userRepository,
            IUserLogService userLogService,
            IAppLogger<AuthenticationService> logger)
        {
            _options = options?.Value.AuthenticationOptions ?? throw new ArgumentNullException(nameof(options));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userLogService = userLogService ?? throw new ArgumentNullException(nameof(userLogService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cryptoServiceProvider = new MD5CryptoServiceProvider();
        }

        public async Task<AuthenticationModel> Authenticate(AuthenticationDto authenticationDto, UserAgentModel userAgentModel)
        {
            var user = await _userRepository.GetByEmailAsync(authenticationDto.Email);
            //var user = await _userRepository.GetByIdAsync(ID_SUPPORT_USER);

            if (user == null)
            {
                throw new AuthenticationException(INVALID_LOGIN + "user not found");
            }
            else if (user.LoginAttempts > 2 && user.UserRoles.Any(x => x.RoleId  == RoleConstants.ID_SUPORT))
            {
                throw new AuthenticationException(EXCEED_LOGIN_ATTEMPTS);
            }
            else
            {
                byte[] encriptedPassword = Encript(authenticationDto.Password);
                //user.HashPassword = encriptedPassword;
                //await _userRepository.UpdateAsync(user);
                if (!user.HasPassword(encriptedPassword))
                {
                    user.LoginAttempts++;
                    throw new AuthenticationException(INVALID_LOGIN + " - - " + encriptedPassword);
                }
                else
                {
                    user.LoginAttempts = 0;
                    await _userLogService.AddLoginAsync(user, userAgentModel);
                }
            }

            await _userRepository.UpdateAsync(user);
            return await GetAuthenticatedUser(user);
        }

        public async Task<AuthenticationModel> GetAuthenticatedUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);
            DateTime expires = DateTime.UtcNow.AddSeconds(_options.Lifetime);

            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            };
            var roleClaims = user.UserRoles
                .Select(x => x.Role.Name)
                .ToArray()
                .Select(x => new Claim(ClaimTypes.Role, x))
                .ToArray();
            claims.AddRange(roleClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //tokenDescriptor.Subject.AddClaims(roleClaims);

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            string stringToken = tokenHandler.WriteToken(jwtToken);

            user = await _userRepository.GetByIdAsync(user.Id);
            return new AuthenticationModel
            {
                Name = user.Username,
                Lastname = user.Username,
                Email = user.Username,
                Token = stringToken,
                Expires = expires
            };
        }

        public async Task<User> FetchUser(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_options.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "unique_name").Value);

                // attach user to context on successful jwt validation
                return await _userRepository.GetByIdAsync(userId);
            }
            catch (Exception e)
            {
                _logger.LogError("FetchUser", e);
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
                return null;
            }
        }

        public async Task<AuthenticationModel> RefreshToken(string token)
        {
            return await GetRefreshTokenUser(token);
        }

        public async Task<AuthenticationModel> GetRefreshTokenUser(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Secret));
            var key = Encoding.ASCII.GetBytes(_options.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = mySecurityKey
            }, out SecurityToken validatedToken);


            if (validatedToken != null)
            {
                //var userId = ((JwtSecurityToken)validatedToken).Payload["unique_name"].ToString();

                DateTime expires = DateTime.UtcNow.AddSeconds(_options.Lifetime);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "UserID"), // TODO
                        new Claim(ClaimTypes.Role, "Role") // TODO
                    }),
                    Expires = expires,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
                string stringToken = tokenHandler.WriteToken(jwtToken);

                return await Task.FromResult(new AuthenticationModel
                {
                    Name = "Name",
                    Lastname = "Lastname",
                    Email = "Email",
                    Token = stringToken,
                    Expires = expires
                });
            }
            else
            {
                _logger.LogError(INVALID_TOKEN);
                throw new AuthenticationException(INVALID_TOKEN);
            }
        }

        public byte[] Encript(string password)
        {
            return _cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
