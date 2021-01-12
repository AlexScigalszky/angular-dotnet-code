using Application.Models.Base;

namespace Application.Models.Authentication
{
    public class AuthenticationDto : BaseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
    }
}
