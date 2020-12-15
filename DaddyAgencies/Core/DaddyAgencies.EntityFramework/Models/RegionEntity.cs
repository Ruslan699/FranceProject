using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("Regions")]
    public class RegionEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Name")]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [Column("PathString")]
        [StringLength(int.MaxValue)]
        public string PathString { get; set; }

        [Column("MapBounds")]
        [StringLength(128)]
        public string MapBounds { get; set; }

        [Column("MapCenter")]
        [StringLength(128)]
        public string MapCenter { get; set; }

        [Column("MapZoom")]
        [StringLength(128)]
        public string MapZoom { get; set; }

        [Column("Description")]
        [StringLength(500)]
        public string Description { get; set; }

        public virtual List<DepartamentEntity> Departaments { get; set; }
    }
}
