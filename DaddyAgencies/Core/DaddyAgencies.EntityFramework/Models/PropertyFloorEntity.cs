using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("PropertyFloors")]
    public class PropertyFloorEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Name")]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [Column("Number")]
        public int Number { get; set; }


        #region Relations

        [Required]
        [Column("FloorTypeUid")]
        [ForeignKey("FloorType")]
        public Guid FloorTypeUid { get; set; }
        public virtual PropertyFloorTypeEntity FloorType { get; set; }

        #endregion
    }
}
