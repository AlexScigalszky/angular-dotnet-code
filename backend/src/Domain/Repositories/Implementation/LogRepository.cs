using Domain.Data;
using Domain.Entities;
using Infrastructure.Repository.Base;

namespace Domain.Repositories.Implementation
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(ExampleContext dbContext) : base(dbContext)
        {
        }
    }
}