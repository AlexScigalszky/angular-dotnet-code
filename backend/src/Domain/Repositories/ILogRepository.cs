using Core.Repositories.Base;
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<Log> GetByIdAsync(long id);
    }
}
