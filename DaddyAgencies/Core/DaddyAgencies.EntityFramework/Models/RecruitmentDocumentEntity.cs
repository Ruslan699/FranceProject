using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("RecruitmentDocuments")]
    public class RecruitmentDocumentEntity : DocumentBaseEntity
    {
        [Required]
        [Column("RecruitmentUid")]
        [ForeignKey("Recruitment")]
        public Guid RecruitmentUid { get; set; }

        public virtual RecruitmentEntity Recruitment { get; set; }
    }
}