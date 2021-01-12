using Core.Specifications.Base;
using Domain.Entities;
using System.Linq;

namespace Domain.Specifications
{
    public class UserWithRoleSpecification : BaseSpecification<User>
    {
        public UserWithRoleSpecification(string email)
            : base(p =>
                p.Username.ToLower().Equals(email.ToLower())
            )
        {
        }

        public UserWithRoleSpecification(long? id, long? roleId)
            : base(p =>
                id.HasValue ? p.Id == id.Value : true &&
                roleId.HasValue ? p.UserRoles.Any(ur => ur.RoleId == roleId.Value) : true
            )
        {
            AddIncludes();
        }

        public UserWithRoleSpecification(long roleId, string countryId)
            : base(p =>
                p.CountryId  == countryId &&
                p.UserRoles.Any(ur => ur.RoleId == roleId)
            )
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude("UserRoles.Role");
        }
    }
}
