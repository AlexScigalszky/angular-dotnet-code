using Core.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Countries")]
    public partial class Country : EntityBase
    {
        [Key]
        [StringLength(2)]
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string TimeZone { get; set; }
    }
}
