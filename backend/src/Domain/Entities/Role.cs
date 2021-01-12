using Core.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Roles")]
    public partial class Role : EntityBase
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<RolePermission> RolesPermissions { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            RolesPermissions = new HashSet<RolePermission>();
            UserRoles = new HashSet<UserRole>();
        }

        public static Role Create(string name)
        {
            var role = new Role()
            {
                Name = name
            };
            return role;
        }

        public static Role Create(long id, string name)
        {
            var role = Role.Create(name);
            role.Id = id;
            return role;
        }
    }
}
