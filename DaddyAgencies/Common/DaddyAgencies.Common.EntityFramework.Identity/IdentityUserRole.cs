using System;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public class IdentityUserRole : IdentityUserRole<Guid>
    {
    }

    public class IdentityUserRole<TKey>
    {
        public virtual TKey UserId { get; set; }

        public virtual TKey RoleId { get; set; }
    }
}