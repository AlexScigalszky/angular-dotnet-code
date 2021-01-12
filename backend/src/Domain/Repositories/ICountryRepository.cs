using Core.Repositories.Base;
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> GetByIdAsync(string paisId);
    }
}
