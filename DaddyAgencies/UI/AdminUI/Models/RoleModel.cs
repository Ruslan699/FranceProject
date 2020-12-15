using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminUI.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            Users = new List<UserModel>();
        }

        public virtual ICollection<UserModel> Users { get; }

        public Guid Id { get; set; }

        public string Name { get; set; } = "Agent";
    }
}