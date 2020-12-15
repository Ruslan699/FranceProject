using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("PostalCodes")]
    public class PostalCodeEntity : BasePersistenceEntity
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
        [Column("DepartamentUid")]
        [ForeignKey("Departament")]
        public Guid DepartamentUid { get; set; }

        public virtual DepartamentEntity Departament { get; set; }
    }
}