using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.EntityFramework.Models
{
    using Common.EntityFramework;

    [Table("PropertyAdditionalElements")]
    public class PropertyAdditionalElementEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Availability")]
        public bool Availability { get; set; }

        #region Relation

        [Required]
        [Column("PropertyUid")]
        [ForeignKey("Property")]
        public Guid PropertyUid { get; set; }
        public PropertyEntity Property { get; set; }

        [Required]
        [Column("AdditionalElementUid")]
        [ForeignKey("AdditionalElement")]
        public Guid AdditionalElementUid { get; set; }
        public AdditionalElementEntity AdditionalElement { get; set; }

        #endregion

    }
}
