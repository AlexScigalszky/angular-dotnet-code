using Core.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("UserLogs")]
    public partial class UserLog : EntityBase
    {
        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public string Action { get; set; }
    }
}
