using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("RolesPermissions")]
    public partial class RolePermission
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Permission")]
        public long PermissionId { get; set; }
        public Permission Permission { get; set; }

        [ForeignKey("Role")]
        public long RoleId { get; set; }
        public Role Role { get; set; }

        public static RolePermission Create(long roleId, long permissionId)
        {
            var rolePermission = new RolePermission()
            {
                PermissionId = permissionId,
                RoleId = roleId
            };
            return rolePermission;
        }

        public static RolePermission Create(long id, long roleId, long permissionId)
        {
            var rolePermission = RolePermission.Create(roleId, permissionId);
            rolePermission.Id = id;
            return rolePermission;
        }

    }
}
