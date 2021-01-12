using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public partial class ExampleContext : DbContext
    {
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<MailTemplate> MailTemplates { get; set; }

        public ExampleContext()
        {
        }

        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }
    }
}
