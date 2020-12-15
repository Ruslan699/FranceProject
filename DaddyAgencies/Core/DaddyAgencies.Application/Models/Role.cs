using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Models
{
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        public virtual ICollection<User> Users { get; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
