using Core.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public partial class Log : EntityBase
    {
        public int Module { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Message { get; set; }

        public string Detail { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
