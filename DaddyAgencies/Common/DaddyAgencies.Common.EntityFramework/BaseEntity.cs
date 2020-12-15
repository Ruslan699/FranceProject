using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaddyAgencies.Common.EntityFramework
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        [Column("Uid")]
        public Guid Uid { get; set; }
    }
}
