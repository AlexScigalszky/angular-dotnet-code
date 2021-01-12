using Domain.Data;
using Domain.Entities;
using Infrastructure.Repository.Base;

namespace Domain.Repositories.Implementation
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ExampleContext dbContext) : base(dbContext)
        {
        }
    }
}
