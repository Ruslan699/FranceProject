using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;
using DaddyAgencies.Common.EntityFramework.Identity;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("PhysicalPersons")]
    public class PhysicalPersonEntity : BasePersistenceEntity
    {
        [Required]
        [Column("Surname")]
        [StringLength(250)]
        public string Surname { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [Column("LastName")]
        [StringLength(250)]
        public string LastName { get; set; }

        [Required]
        [Column("Email")]
        [StringLength(128)]
        public string Email { get; set; }

        [Required]
        [Column("PhoneNumber")]
        [StringLength(128)]
        public string PhoneNumber { get; set; }

        [Column("Skype")]
        [StringLength(50)]
        public string Skype { get; set; }

        [Column("DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        [Column("Gender")]
        public bool? Gender { get; set; }

        [Column("Address")]
        [StringLength(500)]
        public string Address { get; set; }

        [Column("UserUid")]
        [ForeignKey("User")]
        public Guid? UserUid { get; set; }

        public IdentityUser User { get; set; }
    }
}
