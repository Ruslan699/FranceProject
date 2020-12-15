using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public class UserStore<TUser> :
        UserStore<TUser, IdentityRole, Guid, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> 
        where TUser : IdentityUser
    {
        public UserStore()
            : this(new IdentityDbContext())
        {
            DisposeContext = true;
        }
        
        public UserStore(DbContext context)
            : base(context)
        {
        }
    }
    
    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> :
        IUserLoginStore<TUser, TKey>,
        IUserClaimStore<TUser, TKey>,
        IUserRoleStore<TUser, TKey>,
        IUserPasswordStore<TUser, TKey>,
        IUserSecurityStampStore<TUser, TKey>,
        IQueryableUserStore<TUser, TKey>,
        IUserEmailStore<TUser, TKey>,
        IUserPhoneNumberStore<TUser, TKey>,
        IUserTwoFactorStore<TUser, TKey>,
        IUserLockoutStore<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole>
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserClaim : IdentityUserClaim<TKey>, new()
    {
        private readonly IDbSet<TUserLogin> _logins;
        private readonly EntityStore<TRole> _roleStore;
        private readonly IDbSet<TUserClaim> _userClaims;
        private readonly IDbSet<TUserRole> _userRoles;
        private bool _disposed;
        private EntityStore<TUser> _userStore;
        
        public UserStore(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            AutoSaveChanges = true;
            _userStore = new EntityStore<TUser>(context);
            _roleStore = new EntityStore<TRole>(context);
            _logins = Context.Set<TUserLogin>();
            _userClaims = Context.Set<TUserClaim>();
            _userRoles = Context.Set<TUserRole>();
        }
        
        public DbContext Context { get; private set; }
        
        public bool DisposeContext { get; set; }
        
        public bool AutoSaveChanges { get; set; }
        
        public IQueryable<TUser> Users => _userStore.EntitySet;
        
        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await EnsureClaimsLoaded(user).WithCurrentCulture();
            return user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }
        
        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            _userClaims.Add(new TUserClaim { UserId = user.Id, ClaimType = claim.Type, ClaimValue = claim.Value });
            return Task.FromResult(0);
        }
        
        public virtual async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            IEnumerable<TUserClaim> claims;
            var claimValue = claim.Value;
            var claimType = claim.Type;
            if (AreClaimsLoaded(user))
            {
                claims = user.Claims.Where(uc => uc.ClaimValue == claimValue && uc.ClaimType == claimType).ToList();
            }
            else
            {
                var userId = user.Id;
                claims = await _userClaims.Where(uc => uc.ClaimValue == claimValue && uc.ClaimType == claimType && uc.UserId.Equals(userId)).ToListAsync().WithCurrentCulture();
            }
            foreach (var c in claims)
            {
                _userClaims.Remove(c);
            }
        }
        
        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.EmailConfirmed);
        }
        
        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }
        
        public virtual Task SetEmailAsync(TUser user, string email)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Email = email;
            return Task.FromResult(0);
        }
        
        public virtual Task<string> GetEmailAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }
        
        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper());
        }
        
        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }
        
        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }
        
        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }
        
        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }
        
        public virtual Task<int> GetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.AccessFailedCount);
        }
        
        public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEnabled);
        }
        
        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }
        
        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Id.Equals(userId));
        }
        
        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.UserName.ToUpper() == userName.ToUpper());
        }
        
        public virtual async Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Create(user);
            await SaveChanges().WithCurrentCulture();
        }
        
        public virtual async Task DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Delete(user);
            await SaveChanges().WithCurrentCulture();
        }
        
        public virtual async Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Update(user);
            await SaveChanges().WithCurrentCulture();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public virtual async Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            var userLogin =
                await _logins.FirstOrDefaultAsync(l => l.LoginProvider == provider && l.ProviderKey == key).WithCurrentCulture();
            if (userLogin != null)
            {
                var userId = userLogin.UserId;
                return await GetUserAggregateAsync(u => u.Id.Equals(userId)).WithCurrentCulture();
            }
            return null;
        }
        
        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            _logins.Add(new TUserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            });
            return Task.FromResult(0);
        }
        
        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            TUserLogin entry;
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            if (AreLoginsLoaded(user))
            {
                entry = user.Logins.SingleOrDefault(ul => ul.LoginProvider == provider && ul.ProviderKey == key);
            }
            else
            {
                var userId = user.Id;
                entry = await _logins.SingleOrDefaultAsync(ul => ul.LoginProvider == provider && ul.ProviderKey == key && ul.UserId.Equals(userId)).WithCurrentCulture();
            }
            if (entry != null)
            {
                _logins.Remove(entry);
            }
        }
        
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await EnsureLoginsLoaded(user).WithCurrentCulture();
            return user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
        }
        
        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        
        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }
        
        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
        
        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }
        
        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumber);
        }
        
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }
        
        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }
        
        public virtual async Task AddToRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", nameof(roleName));
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (roleEntity == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                    "IdentityResources.RoleNotFound", roleName));
            }

            var ur = new TUserRole { UserId = user.Id, RoleId = roleEntity.Id };
            _userRoles.Add(ur);
        }
        
        public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", nameof(roleName));
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (roleEntity != null)
            {
                var roleId = roleEntity.Id;
                var userId = user.Id;
                var userRole = await _userRoles.FirstOrDefaultAsync(r => roleId.Equals(r.RoleId) && r.UserId.Equals(userId)).WithCurrentCulture();
                if (userRole != null)
                {
                    _userRoles.Remove(userRole);
                }
            }
        }
        
        public virtual async Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;
            var query = from userRole in _userRoles
                        where userRole.UserId.Equals(userId)
                        join role in _roleStore.DbEntitySet on userRole.RoleId equals role.Id
                        select role.Name;
            return await query.ToListAsync().WithCurrentCulture();
        }
        
        public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", nameof(roleName));
            }
            var role = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (role != null)
            {
                var userId = user.Id;
                var roleId = role.Id;
                return await _userRoles.AnyAsync(ur => ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId)).WithCurrentCulture();
            }
            return false;
        }
        
        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        
        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.SecurityStamp);
        }
        
        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }
        
        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }
        
        private async Task SaveChanges()
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync().WithCurrentCulture();
            }
        }

        private bool AreClaimsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Claims).IsLoaded;
        }

        private async Task EnsureClaimsLoaded(TUser user)
        {
            if (!AreClaimsLoaded(user))
            {
                var userId = user.Id;
                await _userClaims.Where(uc => uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture();
                Context.Entry(user).Collection(u => u.Claims).IsLoaded = true;
            }
        }

        private async Task EnsureRolesLoaded(TUser user)
        {
            if (!Context.Entry(user).Collection(u => u.Roles).IsLoaded)
            {
                var userId = user.Id;
                await _userRoles.Where(uc => uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture();
                Context.Entry(user).Collection(u => u.Roles).IsLoaded = true;
            }
        }

        private bool AreLoginsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Logins).IsLoaded;
        }

        private async Task EnsureLoginsLoaded(TUser user)
        {
            if (!AreLoginsLoaded(user))
            {
                var userId = user.Id;
                await _logins.Where(uc => uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture();
                Context.Entry(user).Collection(u => u.Logins).IsLoaded = true;
            }
        }

        protected virtual async Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
        {
            TUser user;
            if (FindByIdFilterParser.TryMatchAndGetId(filter, out var id))
            {
                user = await _userStore.GetByIdAsync(id).WithCurrentCulture();
            }
            else
            {
                user = await Users.FirstOrDefaultAsync(filter).WithCurrentCulture();
            }
            if (user != null)
            {
                await EnsureClaimsLoaded(user).WithCurrentCulture();
                await EnsureLoginsLoaded(user).WithCurrentCulture();
                await EnsureRolesLoaded(user).WithCurrentCulture();
            }
            return user;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing)
            {
                Context?.Dispose();
            }
            _disposed = true;
            Context = null;
            _userStore = null;
        }
        
        private static class FindByIdFilterParser
        {
            private static readonly Expression<Func<TUser, bool>> Predicate = u => u.Id.Equals(default(TKey));
            // ReSharper disable once StaticMemberInGenericType
            private static readonly MethodInfo EqualsMethodInfo = ((MethodCallExpression)Predicate.Body).Method;
            // ReSharper disable once StaticMemberInGenericType
            private static readonly MemberInfo UserIdMemberInfo = ((MemberExpression)((MethodCallExpression)Predicate.Body).Object)?.Member;

            internal static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out TKey id)
            {
                id = default(TKey);

                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }

                var callExpression = (MethodCallExpression)filter.Body;
                if (callExpression.Method != EqualsMethodInfo)
                {
                    return false;
                }

                if (callExpression.Object == null
                    || callExpression.Object.NodeType != ExpressionType.MemberAccess
                    || ((MemberExpression)callExpression.Object).Member != UserIdMemberInfo)
                {
                    return false;
                }

                if (callExpression.Arguments.Count != 1)
                {
                    return false;
                }

                MemberExpression fieldAccess;
                if (callExpression.Arguments[0].NodeType == ExpressionType.Convert)
                {
                    var convert = (UnaryExpression)callExpression.Arguments[0];
                    if (convert.Operand.NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    fieldAccess = (MemberExpression)convert.Operand;
                }
                else if (callExpression.Arguments[0].NodeType == ExpressionType.MemberAccess)
                {
                    fieldAccess = (MemberExpression)callExpression.Arguments[0];
                }
                else
                {
                    return false;
                }

                if (fieldAccess.Member.MemberType != MemberTypes.Field
                    || fieldAccess.Expression.NodeType != ExpressionType.Constant)
                {
                    return false;
                }

                var fieldInfo = (FieldInfo)fieldAccess.Member;
                var closure = ((ConstantExpression)fieldAccess.Expression).Value;

                id = (TKey)fieldInfo.GetValue(closure);
                return true;
            }
        }
    }
}