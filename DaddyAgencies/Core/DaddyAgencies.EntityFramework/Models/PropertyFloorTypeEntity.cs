using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.EntityFramework.Models
{
    using Common.EntityFramework;

    [Table("PropertyFloorTypes")]
    public class PropertyFloorTypeEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Name")]
        [StringLength(128)]
        public string Name { get; set; }

        [Column("Description")]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
