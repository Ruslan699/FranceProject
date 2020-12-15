using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.EntityFramework.Models
{
    using Common.EntityFramework;

    [Table("AdditionalElements")]
    public class AdditionalElementEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Name")]
        [StringLength(128)]
        public string Name { get; set; }

        [Column("Icon")]
        public byte[] Icon { get; set; }

        [Column("Description")]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
