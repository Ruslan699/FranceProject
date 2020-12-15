using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DaddyAgencies.Common.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("Recruitments")]
    public class RecruitmentEntity : BasePersistenceEntity
    {
        [Required]
        [Column("FullName")]
        [StringLength(500)]
        public string FullName { get; set; }

        [Required]
        [Column("PhoneNumber")]
        [StringLength(128)]
        public string PhoneNumber { get; set; }

        [Required]
        [Column("Email")]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        [Column("Message")]
        [StringLength(1000)]
        public string Message { get; set; }

        public virtual List<RecruitmentDocumentEntity> Documents { get; set; }
    }
}
