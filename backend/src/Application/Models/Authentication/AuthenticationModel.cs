using System;

namespace Application.Models.Authentication
{
    public class AuthenticationModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
    }
}
