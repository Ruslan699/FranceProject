using DaddyAgencies.Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("MainDepartments")]
    public class MainDepartmentEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Name")]
        [MaxLength(15)]
        public string Name { get; set; }

        [Column("Description")]
        [MaxLength(15)]
        public string Description { get; set; }
    }
}
