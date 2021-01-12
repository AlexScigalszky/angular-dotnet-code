using Application.Models.Base;

namespace Application.Models.UserRole
{
    public class UserRoleModel : BaseModel
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public long RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
