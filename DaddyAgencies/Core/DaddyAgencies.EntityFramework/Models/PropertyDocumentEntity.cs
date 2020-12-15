using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("PropertyDocuments")]
    public class PropertyDocumentEntity : DocumentBaseEntity
    {
        [Required]
        [Column("PropertyUid")]
        [ForeignKey("Property")]
        public Guid PropertyUid { get; set; }
        public int RowNo { get; set; }
        public virtual PropertyEntity Property { get; set; }
    }
}
