using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public partial class MailTemplate : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Template { get; set; }

        [Required]
        public string Subject { get; set; }

        internal static MailTemplate Create(long id, string name, string subject, string template)
        {
            var mail = new MailTemplate()
            {
                Id = id,
                Name = name,
                Subject = subject,
                Template = template
            };
            return mail;
        }
    }
}
