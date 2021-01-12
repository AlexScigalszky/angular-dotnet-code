using Domain.Data;
using Domain.Entities;
using Domain.Specifications;
using Infrastructure.Repository.Base;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class MailTemplateRepository : Repository<MailTemplate>, IMailTemplateRepository
    {
        public MailTemplateRepository(ExampleContext dbContext) : base(dbContext)
        {
        }

        public async Task<MailTemplate> GetByIdAsync(long id)
        {
            var spec = new MailTemplateSpecification(id);
            return await FirstAsync(spec);
        }
    }
}
