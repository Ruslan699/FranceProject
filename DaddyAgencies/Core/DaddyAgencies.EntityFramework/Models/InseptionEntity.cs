using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;
using DaddyAgencies.Common.EntityFramework.Identity;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("Inseptions")]
    public class InseptionEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Status")]
        public int Status { get; set; }
        
        [Column("Description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column("RequestedDateUtc")]
        public DateTime RequestedDateUtc { get; set; }

        [Column("ConfirmedDateUtc")]
        public DateTime? ConfirmedDateUtc { get; set; }

        [StringLength(500)]
        [Column("ConfirmedAddress")]
        public string ConfirmedAddress { get; set; }

        [Column("PropertyUid")]
        [ForeignKey("Property")]
        public Guid? PropertyUid { get; set; }

        public virtual PropertyEntity Property { get; set; }

        [Column("PostalCodeUid")]
        [ForeignKey("PostalCode")]
        public Guid? PostalCodeUid { get; set; }
        public virtual PostalCodeEntity PostalCode { get; set; }

        [Required]
        [Column("RegionUid")]
        [ForeignKey("Region")]
        public Guid RegionUid { get; set; }

        public virtual RegionEntity Region { get; set; }

        [Column("RequesterUserUid")]
        [ForeignKey("RequesterUser")]
        public Guid? RequesterUserUid { get; set; }
        
        public IdentityUser RequesterUser { get; set; }

        [Column("ConfirmerUserUid")]
        [ForeignKey("ConfirmerUser")]
        public Guid? ConfirmerUserUid { get; set; }

        public IdentityUser ConfirmerUser { get; set; }

        [Required]
        [Column("CustomerFullName")]
        [StringLength(250)]
        public string CustomerFullName { get; set; }

        [Required]
        [Column("CustomerEmail")]
        [StringLength(50)]
        public string CustomerEmail { get; set; }

        [Required]
        [Column("CustomerPhone")]
        [StringLength(50)]
        public string CustomerPhone { get; set; }
    }
}
