// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Data.Entity;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    public class RoleStore<TRole> : RoleStore<TRole, Guid, IdentityUserRole>
        where TRole : IdentityRole, new()
    {
        public RoleStore()
            : base(new IdentityDbContext())
        {
            DisposeContext = true;
        }
        public RoleStore(DbContext context) : base(context)
        {
        }
    }
    
    public class RoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>
        where TUserRole : IdentityUserRole<TKey>, new()
        where TRole : IdentityRole<TKey, TUserRole>, new()
    {
        private bool _disposed;
        private EntityStore<TRole> _roleStore;

        public RoleStore(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _roleStore = new EntityStore<TRole>(context);
        }
        
        public DbContext Context { get; private set; }
        
        public bool DisposeContext { get; set; }
        
        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return _roleStore.GetByIdAsync(roleId);
        }
        
        public Task<TRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return _roleStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper() == roleName.ToUpper());
        }
        
        public virtual async Task CreateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            _roleStore.Create(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }
        
        public virtual async Task DeleteAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            _roleStore.Delete(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }
        
        public virtual async Task UpdateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            _roleStore.Update(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public IQueryable<TRole> Roles => _roleStore.EntitySet;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing)
                Context?.Dispose();

            _disposed = true;
            Context = null;
            _roleStore = null;
        }
    }
}