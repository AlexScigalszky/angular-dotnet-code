using Application.Models.Base;

namespace Application.Models.Authentication
{
    public class ForgotPasswordDto : BaseDto
    {
        public string Email { get; set; }
    }
}
