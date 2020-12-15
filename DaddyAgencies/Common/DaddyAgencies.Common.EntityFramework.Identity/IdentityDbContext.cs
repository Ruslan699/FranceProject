using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public class IdentityDbContext :
        IdentityDbContext<IdentityUser, IdentityRole, Guid, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }

        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public static IdentityDbContext Create(string connectionName) => new IdentityDbContext(connectionName);


        //burda
    }

    public class IdentityDbContext<TUser> :
        IdentityDbContext<TUser, IdentityRole, Guid, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TUser : IdentityUser
    {
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }

        public IdentityDbContext(string nameOrConnectionString)
            : this(nameOrConnectionString, true)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, bool throwIfV1Schema)
            : base(nameOrConnectionString)
        {
            if (throwIfV1Schema && IsIdentityV1Schema(this))
            {
                throw new InvalidOperationException("IdentityResources.IdentityV1SchemaError");
            }
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        internal static bool IsIdentityV1Schema(DbContext db)
        {
            if (!(db.Database.Connection is SqlConnection originalConnection))
                return false;

            if (db.Database.Exists())
            {
                using (var tempConnection = new SqlConnection(originalConnection.ConnectionString))
                {
                    tempConnection.Open();
                    return
                        VerifyColumns(tempConnection, "AspNetUsers", "Id", "UserName", "PasswordHash", "SecurityStamp",
                            "Discriminator") &&
                        VerifyColumns(tempConnection, "AspNetRoles", "Id", "Name") &&
                        VerifyColumns(tempConnection, "AspNetUserRoles", "UserId", "RoleId") &&
                        VerifyColumns(tempConnection, "AspNetUserClaims", "Id", "ClaimType", "ClaimValue", "User_Id") &&
                        VerifyColumns(tempConnection, "AspNetUserLogins", "UserId", "ProviderKey", "LoginProvider");
                }
            }

            return false;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities",
            Justification = "Reviewed")]
        internal static bool VerifyColumns(SqlConnection conn, string table, params string[] columns)
        {
            var tableColumns = new List<string>();
            using (
                var command =
                    new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table", conn))
            {
                command.Parameters.Add(new SqlParameter("Table", table));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableColumns.Add(reader.GetString(0));
                    }
                }
            }
            return columns.All(tableColumns.Contains);
        }
    }

    public class IdentityDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : BaseDbContext
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
    {
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }

        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public virtual IDbSet<TUser> Users { get; set; }

        public virtual IDbSet<TRole> Roles { get; set; }

        public bool RequireUniqueEmail { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            var user = modelBuilder.Entity<TUser>()
                .ToTable("WebUsers");
            user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            user.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            user.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

            // CONSIDER: u.Email is Required if set on options?
            user.Property(u => u.Email).HasMaxLength(256);

            modelBuilder.Entity<TUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("WebUserRoles");

            modelBuilder.Entity<TUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("WebUserLogins");

            modelBuilder.Entity<TUserClaim>()
                .ToTable("WebUserClaims");

            var role = modelBuilder.Entity<TRole>()
                .ToTable("WebRoles");
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

            base.OnModelCreating(modelBuilder);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
            IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                if (entityEntry.Entity is TUser user)
                {
                    if (Users.Any(u => String.Equals(u.UserName, user.UserName)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "IdentityResources.DuplicateUserName", user.UserName)));
                    }
                    if (RequireUniqueEmail && Users.Any(u => String.Equals(u.Email, user.Email)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "IdentityResources.DuplicateEmail", user.Email)));
                    }
                }
                else
                {
                    if (entityEntry.Entity is TRole role && Roles.Any(r => String.Equals(r.Name, role.Name)))
                    {
                        errors.Add(new DbValidationError("Role",
                            String.Format(CultureInfo.CurrentCulture, "IdentityResources.RoleAlreadyExists", role.Name)));
                    }
                }
                if (errors.Any())
                {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}