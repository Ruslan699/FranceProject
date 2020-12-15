using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public class IdentityRole : IdentityRole<Guid, IdentityUserRole>
    {
        public IdentityRole()
        {
            Id = Guid.NewGuid();
        }

        public IdentityRole(string roleName)
            : this()
        {
            Name = roleName;
        }
        public IdentityRole(Guid uid, string roleName)
            : this()
        {
            Id = uid;
            Name = roleName;
        }
    }


    public class IdentityRole<TKey, TUserRole> : IRole<TKey> where TUserRole : IdentityUserRole<TKey>
    {
        public IdentityRole()
        {
            Users = new List<TUserRole>();
        }

        public virtual ICollection<TUserRole> Users { get; }

        public TKey Id { get; set; }

        public string Name { get; set; }
    }
}