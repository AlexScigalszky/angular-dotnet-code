using Domain.Data;
using Domain.Entities;
using Infrastructure.Repository.Base;

namespace Domain.Repositories.Implementation
{
    public class UserLogRepository : Repository<UserLog>, IUserLogRepository
    {
        public UserLogRepository(ExampleContext dbContext) : base(dbContext)
        {
        }
    }
}
