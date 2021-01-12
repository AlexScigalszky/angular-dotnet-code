using Domain.Data;
using Domain.Entities;
using Domain.Specifications;
using Infrastructure.Repository.Base;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ExampleContext dbContext) : base(dbContext)
        {
        }

        public async Task<Country> GetByIdAsync(string paisId)
        {
            var spec = new CountrySpecification(paisId);
            return await FirstAsync(spec);
        }
    }
}
