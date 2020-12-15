using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
