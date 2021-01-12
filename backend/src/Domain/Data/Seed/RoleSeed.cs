using Domain.Constants;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Data.Seeds
{
    public class RoleSeed
    {
        public static IEnumerable<Role> GetData()
        {
            return new Role[]
            {
                Role.Create(RoleConstants.ID_SUPORT, RoleConstants.SUPORT),
            };
        }
    }
}
