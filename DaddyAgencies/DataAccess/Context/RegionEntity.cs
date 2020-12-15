using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Context
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlDefaultValueAttribute : Attribute
    {
        public string DefaultValue { get; set; }
    }

    internal abstract class BaseEntity : RegionEntity
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }

        [Required]
        [Column("Created")]
        [SqlDefaultValue(DefaultValue = "getutcdate()")]
        public DateTime Created { get; set; }

        [Required]
        [Column("Modified")]
        public DateTime Modified { get; set; }

        [Required]
        [Column("Deleted")]
        public bool Deleted { get; set; }
    }

    [Table("Regions")]
    internal class RegionEntity
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }

    internal class PropertyEntity
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }
    }
}
