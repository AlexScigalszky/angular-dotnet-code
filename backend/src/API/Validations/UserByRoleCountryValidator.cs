using Application.Models.User;
using FluentValidation;

namespace AspnetRun.Api.Application.Validations
{
    public class UserByRoleCountryValidator : AbstractValidator<UserDto>
    {
        public UserByRoleCountryValidator()
        {
            RuleFor(request => request.RoleId).NotNull();
            RuleFor(request => request.CountryId).NotNull().NotEmpty();
        }
    }
}
