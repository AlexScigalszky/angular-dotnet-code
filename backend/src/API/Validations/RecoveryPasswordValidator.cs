using Application.Models.Authentication;
using FluentValidation;

namespace API.Validations
{
    public class RecoveryPasswordValidator : AbstractValidator<RecoveryPasswordDto>
    {
        public RecoveryPasswordValidator()
        {
            RuleFor(request => request.Secret).NotEmpty().NotNull();
            RuleFor(request => request.Password).NotEmpty().NotNull();
            RuleFor(request => request.ConfirmPassword).NotEmpty().NotNull().Equal(request => request.Password);
        }
    }
}
