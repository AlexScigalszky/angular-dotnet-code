using Application.Models.Authentication;
using FluentValidation;

namespace AspnetRun.Api.Application.Validations
{
    public class AuthenticationValidator : AbstractValidator<AuthenticationDto>
    {
        public AuthenticationValidator()
        {
            RuleFor(request => request.Email).NotEmpty().NotNull();
            RuleFor(request => request.Password).NotEmpty().NotNull();
            RuleFor(request => request.Pin).NotEmpty().NotNull();
        }
    }
}
