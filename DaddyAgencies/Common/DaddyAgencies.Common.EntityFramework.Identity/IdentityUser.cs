using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public class IdentityUser : IdentityUser<Guid, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public IdentityUser()
        {
            Id = Guid.NewGuid();
        }

        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IdentityUser, Guid> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim(Identity.Claims.PhoneNumber, PhoneNumber));
            userIdentity.AddClaim(new Claim(Identity.Claims.Uid, Id.ToString()));
            return userIdentity;
        }
    }

    public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
        where TLogin : IdentityUserLogin<TKey>
        where TRole : IdentityUserRole<TKey>
        where TClaim : IdentityUserClaim<TKey>
    {
        public IdentityUser()
        {
            Claims = new List<TClaim>();
            Roles = new List<TRole>();
            Logins = new List<TLogin>();
        }
        
        public TKey Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public virtual ICollection<TRole> Roles { get; }

        public virtual ICollection<TClaim> Claims { get; }

        public virtual ICollection<TLogin> Logins { get; }
    }
}