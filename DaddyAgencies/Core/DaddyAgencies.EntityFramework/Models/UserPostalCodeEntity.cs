using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.EntityFramework.Models
{
    [Table("UserPostalCodes")]
    public class UserPostalCodeEntity : BasePersistenceEntity
    {
        [Required]
        public Guid UserUid { get; set; }

        [Required]
        public Guid PostalCodeUid { get; set; }
    }
}
