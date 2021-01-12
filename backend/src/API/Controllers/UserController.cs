using API.Controllers.Base;
using Application.Interfaces;
using Application.Models.Authentication;
using Application.Models.User;
using AspnetRun.Api.Application.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ExampleController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public UserController(IAuthenticationService authenticationService, IUserService userService) : base()
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AuthenticationModel))]
        public async Task<ActionResult> Login([FromBody]AuthenticationDto authenticationDto)
        {
            var userAgentModel = UserAgentData();
            var authenticationModel = await _authenticationService.Authenticate(authenticationDto, userAgentModel);

            return Ok(authenticationModel);
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<ActionResult> ForgotPassword([FromBody]ForgotPasswordDto forgotPasswordDto)
        {
            var userAgentModel = UserAgentData();
            var success = await _userService.ForgotPassword(forgotPasswordDto, userAgentModel);

            return Ok(success);
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(bool))]
        public async Task<ActionResult> PasswordRecovery([FromBody]RecoveryPasswordDto recoveryPasswordDto)
        {
            var userAgentModel = UserAgentData();
            var success = await _userService.PasswordRecovery(recoveryPasswordDto, userAgentModel);

            return Ok(success);
        }

        [Route("[action]")]
        [HttpGet]
        [Filters.Authorize]
        public async Task<ActionResult> Logout()
        {
            var userAgentModel = UserAgentData();
            var user = CurrentUser();
            await _userService.LogoutAsync(user, userAgentModel);

            return Ok();
        }

        [Route("[action]")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserModel))]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersByRole([FromQuery] UserDto userDto)
        {
            var users = await _userService.GetUsersByRole(userDto.RoleId.Value);

            return Ok(users);
        }

        [Route("[action]")]
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserModel))]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersByRoleCountry([FromQuery] UserDto userDto)
        {
            var users = await _userService.GetUsersByRoleCountry(userDto.RoleId.Value, userDto.CountryId);

            return Ok(users);
        }

        [Route("[action]")]
        [HttpGet]
        [Filters.Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AuthenticationModel))]
        public async Task<ActionResult> AuthenticatedUser()
        {
            var user = CurrentUser();
            var authenticationModel = await _authenticationService.GetAuthenticatedUser(user);

            return Ok(authenticationModel);
        }
    }
}
