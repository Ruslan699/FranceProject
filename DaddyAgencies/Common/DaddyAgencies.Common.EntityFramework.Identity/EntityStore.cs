using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DaddyAgencies.Common.EntityFramework.Identity
{
    internal class EntityStore<TEntity> where TEntity : class
    {
        public EntityStore(DbContext context)
        {
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        public DbContext Context { get; }

        public IQueryable<TEntity> EntitySet => DbEntitySet;

        public DbSet<TEntity> DbEntitySet { get; }

        public virtual Task<TEntity> GetByIdAsync(object id) => DbEntitySet.FindAsync(id);

        public void Create(TEntity entity) => DbEntitySet.Add(entity);

        public void Delete(TEntity entity) => DbEntitySet.Remove(entity);

        public virtual void Update(TEntity entity)
        {
            if (entity != null)
                Context.Entry(entity).State = EntityState.Modified;
        }
    }
}