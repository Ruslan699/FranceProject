using System;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public class IdentityUserLogin : IdentityUserLogin<Guid>
    {
    }

    public class IdentityUserLogin<TKey>
    {
        public virtual string LoginProvider { get; set; }

        public virtual string ProviderKey { get; set; }

        public virtual TKey UserId { get; set; }
    }
}