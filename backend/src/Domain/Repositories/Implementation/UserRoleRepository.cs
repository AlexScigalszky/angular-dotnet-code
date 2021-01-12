using Domain.Data;
using Domain.Entities;
using Infrastructure.Repository.Base;

namespace Domain.Repositories.Implementation
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ExampleContext dbContext) : base(dbContext)
        {
        }
    }
}
