using Application.Models.User;
using FluentValidation;

namespace AspnetRun.Api.Application.Validations
{
    public class UserByRoleValidator : AbstractValidator<UserDto>
    {
        public UserByRoleValidator()
        {
            RuleFor(request => request.RoleId).NotNull();
        }
    }
}
