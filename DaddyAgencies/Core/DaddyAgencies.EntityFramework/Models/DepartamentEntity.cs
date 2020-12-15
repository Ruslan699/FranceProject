using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("Departaments")]
    public class DepartamentEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Name")]
        [StringLength(128)]
        public string Name { get; set; }
        
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

        [Required]
        [Column("RegionUid")]
        [ForeignKey("Region")]
        public Guid RegionUid { get; set; }

        public virtual RegionEntity Region { get; set; }

        public virtual List<PropertyEntity> Properties { get; set; }

        public virtual List<PostalCodeEntity> PostalCodes { get; set; }
    }
}