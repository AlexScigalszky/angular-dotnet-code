using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Permissions")]
    public partial class Permission
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [InverseProperty("Permission")]
        public virtual ICollection<RolePermission> RolesPermissions { get; set; }

        public Permission()
        {
            RolesPermissions = new HashSet<RolePermission>();
        }

        public static Permission Create(string name, string description)
        {
            var permission = new Permission()
            {
                Name = name,
                Description = description
            };
            return permission;
        }

        public static Permission Create(long id, string name, string description)
        {
            var permission = Permission.Create(name, description);
            permission.Id = id;
            return permission;
        }
    }
}
