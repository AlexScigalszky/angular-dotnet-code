using Core.Specifications.Base;
using Domain.Entities;

namespace Domain.Specifications
{
    public class MailTemplateSpecification : BaseSpecification<MailTemplate>
    {
        public MailTemplateSpecification(long id)
            : base(p => p.Id.Equals(id))
        {
        }
    }
}
