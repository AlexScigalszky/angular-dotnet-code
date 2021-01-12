using Application.Models.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleModel>> GetRoleList();
    }
}
