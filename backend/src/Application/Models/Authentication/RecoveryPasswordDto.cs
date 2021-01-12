using Application.Models.Base;

namespace Application.Models.Authentication
{
    public class RecoveryPasswordDto : BaseDto
    {
        public string Email { get; set; }
        public string Secret { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
