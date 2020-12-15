using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DaddyAgencies.Common.EntityFramework.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace WebApp
{
    public class ApplicationUserManager : UserManager<IdentityUser, Guid>
    {
        public ApplicationUserManager(IUserStore<IdentityUser, Guid> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<IdentityUser>(new IdentityDbContext("ProjectConnectionString")));

            manager.UserValidator = new UserValidator<IdentityUser, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<IdentityUser, Guid>(dataProtectionProvider.Create("ded8da02-01de-47c9-8462-96f94e6d7370"));
            }
            return manager;
        }
    }

    public class ApplicationSignInManager : SignInManager<IdentityUser, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(IdentityUser user) =>
            user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context) =>
            new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }
}
