using Core.Repositories.Base;
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMailTemplateRepository : IRepository<MailTemplate>
    {
        Task<MailTemplate> GetByIdAsync(long id);
    }
}
