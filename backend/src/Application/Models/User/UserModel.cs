using Application.Models.Base;
using Application.Models.UserRole;
using System;
using System.Collections.Generic;

namespace Application.Models.User
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public DateTime DatePassword { get; set; }
        public bool FirstTime { get; set; }
        public bool Active { get; set; }
        public int LoginAttempts { get; set; }
        public long RoleId { get; set; }
        public string CountryId { get; set; }
        public virtual ICollection<UserRoleModel> UserRoles { get; set; }
    }
}
