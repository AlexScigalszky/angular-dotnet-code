using Domain.Data;
using Domain.Entities;
using Domain.Specifications;
using Infrastructure.Repository.Base;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(ExampleContext dbContext) : base(dbContext)
        {
        }

        public override async Task<User> GetByIdAsync(long id)
        {
            var spec = new UserWithRoleSpecification(id: id, roleId: null);
            return await FirstAsync(spec);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var spec = new UserWithRoleSpecification(email);
            return await FirstAsync(spec);
        }
    }
}
