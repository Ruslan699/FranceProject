using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.EntityFramework.Models
{
    public abstract class DocumentBaseEntity : BasePersistenceEntity
    {
        [Required]
        [StringLength(500)]
        [Column("FileName")]
        public string FileName { get; set; }

        [Required]
        [Column("Type")]
        public int Type { get; set; }

        [Required]
        [StringLength(25)]
        [Column("Extension")]
        public string Extension { get; set; }

        [Required]
        [Column("Body")]
        public byte[] Body { get; set; }
    }
}
