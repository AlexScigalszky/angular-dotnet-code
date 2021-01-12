using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Users")]
    public partial class User : EntityBase
    {
        public string Username { get; set; }

        public byte[] Password { get; set; }

        public DateTime DatePassword { get; set; }

        public bool FirstTime { get; set; }

        public bool Active { get; set; }

        public int LoginAttempts { get; set; }

        public long RoleId { get; set; }

        [ForeignKey("Country")]
        public string CountryId { get; set; }
        public Country Country { get; set; }

        [InverseProperty("Usuario")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public static User Create(long id, string username)
        {
            var user = new User
            {
                Id = id,
                Username = username,
                FirstTime = true,
            };
            return user;
        }

        public static User Create(long id, string username, byte[] password)
        {
            var user = Create(id, username);
            user.Password = password;
            user.DatePassword = DateTimeOffset.UtcNow.Date;
            return user;
        }

        public bool HasPassword(byte[] password)
        {
            bool bEqual = false;
            if (Password.Length == password.Length)
            {
                int i = 0;
                while ((i < Password.Length) && (Password[i] == password[i]))
                {
                    i += 1;
                }
                if (i == Password.Length)
                {
                    bEqual = true;
                }
            }

            return bEqual;
        }
    }
}
