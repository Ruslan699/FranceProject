using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DaddyAgencies.Common.Application.Features;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.EntityFramework;
using DaddyAgencies.Common.Util;

namespace DaddyAgencies.EntityFramework.Features
{
    public abstract class RequestHandler
    {
        protected readonly IMapper Mapper;

        protected RequestHandler(IMapper mapper) => Mapper = mapper;

        public virtual async Task<Result> HandleDeleteRequest<TEntity>(DeleteRequest request,
            CancellationToken cancellationToken)
            where TEntity : BasePersistenceEntity, new()
        {
            using (var db = new ProjectDbContext())
            {
                var entity = await db.Set<TEntity>().
                    FirstAsync(x => x.Uid == request.Uid, cancellationToken);

                entity.IsDeleted = true;
                entity.ModifiedUtc = DateTime.UtcNow;
                await db.SaveChangesAsync(cancellationToken);
            }
            return request.AsResult();
        }

        public virtual async Task<Result> HandleRemoveRequest<TEntity>(RemoveRequest request,
            CancellationToken cancellationToken)
            where TEntity : BasePersistenceEntity, new()
        {
            var entity = new TEntity { Uid = request.Uid };

            using (var db = new ProjectDbContext())
            {
                db.Set<TEntity>().Attach(entity);
                db.Entry(entity).State = EntityState.Deleted;
                await db.SaveChangesAsync(cancellationToken);
            }

            return request.AsResult();
        }

        public virtual async Task<Result<Guid>> HandleSaveRequest<TEntity>(SaveRequest request, 
            CancellationToken cancellationToken)
            where TEntity : BasePersistenceEntity, new()
        {
            TEntity dbEntity;
            using (var db = new ProjectDbContext())
            {
                if (request.Uid == default(Guid))
                {
                    dbEntity = Mapper.Map<TEntity>(request);
                    db.Set<TEntity>().Add(dbEntity);
                }
                else
                {
                    dbEntity = await db.Set<TEntity>().FirstAsync(x => x.Uid == request.Uid, cancellationToken);
                    Mapper.Map(request, dbEntity);
                }
                await db.SaveChangesAsync(cancellationToken);
            }

            return request.AsResultOf(dbEntity.Uid);
        }

        public virtual async Task<Result<TModel>> HandleFindByRequest<TEntity, TModel>(Request<TModel> request, 
            Expression<Func<TEntity, bool>> findBy, CancellationToken cancellationToken)
            where TEntity : BasePersistenceEntity, new()
        {
            Result<TModel> model;
            using (var db = new ProjectDbContext())
            {
                model = await db.Set<TEntity>()
                    .AsNoTracking()
                    .ProjectToFirstOrDefaultAsync<TEntity, TModel>(findBy, Mapper.ConfigurationProvider)
                    .Map(request.AsResultOf);
            }
            return model;
        }

        public virtual async Task<ResultOfCollection<TModel>> GetAll<TEntity, TModel>(GetRequest<TModel> request, 
            CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : BasePersistenceEntity, new()
        {
            ResultOfCollection<TModel> models;
            using (var db = new ProjectDbContext())
            {
                var queryable = db.Set<TEntity>()
                    .AsNoTracking()
                    .Where(c => request.IcludeInactivcve || !c.IsDeleted);

                if (predicate != null)
                    queryable = queryable.Where(predicate);

                models = await queryable.ProjectToListAsync<TModel>(Mapper.ConfigurationProvider)
                .Map(request.AsResultOfCollection);
            }

            return models;
        }

        public virtual async Task<ResultOfCollection<TModel>> HandleGetRequest<TEntity, TModel>(GetRequest<TModel> request,
            CancellationToken cancellationToken, params Expression<Func<TEntity, bool>>[] predicate)
            where TEntity : BasePersistenceEntity, new()
        {
            ResultOfCollection<TModel> models;
            using (var db = new ProjectDbContext())
            {
                var queryable = db.Set<TEntity>()
                    .AsNoTracking()
                    .Where(c => request.IcludeInactivcve || !c.IsDeleted);

                if (predicate != null)
                    queryable = predicate.Aggregate(queryable, (current, expression) => current.Where(expression));

                models = await queryable.ProjectToListAsync<TModel>(Mapper.ConfigurationProvider)
                    .Map(request.AsResultOfCollection);
            }

            return models;
        }

        public virtual async Task<ResultOfCollection<TModel>> HandleGetRequest<TEntity, TModel>(GetRequest<TModel> request,
            CancellationToken cancellationToken, Func<IQueryable<TEntity>, IQueryable<TEntity>> decorate = null)
            where TEntity : BasePersistenceEntity, new()
        {
            ResultOfCollection<TModel> models;
            using (var db = new ProjectDbContext())
            {
                var queryable = db.Set<TEntity>()
                    .AsNoTracking()
                    .Where(c => request.IcludeInactivcve || !c.IsDeleted);

                if (decorate != null)
                    queryable = decorate(queryable);
                models = await queryable.ProjectToListAsync<TModel>(Mapper.ConfigurationProvider)
                    .Map(request.AsResultOfCollection);
            }

            return models;
        }

        public virtual async Task<Result<int>> HandleCountRequest<TEntity>(Request request, CancellationToken cancellationToken,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> decorate = null)
            where TEntity : BasePersistenceEntity, new()
        {
            int totalCount;
            using (var db = new ProjectDbContext())
            {
                var queryable = db.Set<TEntity>()
                    .AsNoTracking()
                    .Where(c => !c.IsDeleted);

                if (decorate != null)
                    queryable = decorate(queryable);

                totalCount = await queryable.CountAsync(cancellationToken);
            }
            return request.AsResultOf(totalCount);
        }
    }

    public abstract class RequestHandler<TEntity> : RequestHandler
        where TEntity : BasePersistenceEntity, new()
    {
        protected RequestHandler(IMapper mapper) : base(mapper)
        {
        }


        public virtual Task<Result> HandleDeleteRequest(DeleteRequest request, CancellationToken cancellationToken)
            => base.HandleDeleteRequest<TEntity>(request, cancellationToken);

        public virtual Task<Result> HandleRemoveRequest(RemoveRequest request, CancellationToken cancellationToken)
            => base.HandleRemoveRequest<TEntity>(request, cancellationToken);

        public virtual Task<Result<Guid>> HandleSaveRequest(SaveRequest request, CancellationToken cancellationToken)
            => base.HandleSaveRequest<TEntity>(request, cancellationToken);

        public virtual Task<Result<TModel>> HandleFindByRequest<TModel>(Request<TModel> request,
            Expression<Func<TEntity, bool>> findBy, CancellationToken cancellationToken) 
            => base.HandleFindByRequest(request, findBy, cancellationToken);

        public virtual Task<ResultOfCollection<TModel>> GetAll<TModel>(GetRequest<TModel> request, 
            CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate = null)
            => base.GetAll(request, cancellationToken, predicate);

        public virtual Task<ResultOfCollection<TModel>> HandleGetRequest<TModel>(GetRequest<TModel> request, 
            CancellationToken cancellationToken, params Expression<Func<TEntity, bool>>[] predicate)
            => base.HandleGetRequest(request, cancellationToken, predicate);

        public virtual Task<ResultOfCollection<TModel>> HandleGetRequest<TModel>(GetRequest<TModel> request, 
            CancellationToken cancellationToken, Func<IQueryable<TEntity>, IQueryable<TEntity>> decorate = null)
            => base.HandleGetRequest(request, cancellationToken, decorate);
    }

    public abstract class RequestHandler<TModel, TEntity> : RequestHandler<TEntity>
    where TEntity : BasePersistenceEntity, new()
    {
        protected RequestHandler(IMapper mapper) : base(mapper) { }

        public virtual Task<Result<TModel>> HandleFindRequest(FindRequest<TModel> request,
            CancellationToken cancellationToken) =>
            HandleFindByRequest(request, x => x.Uid == request.Uid, cancellationToken);

        public virtual Task<Result<TModel>> HandleFindByReques(Request<TModel> request,
            Expression<Func<TEntity, bool>> findBy, CancellationToken cancellationToken)
            => HandleFindByRequest(request, findBy, cancellationToken);

        public virtual Task<ResultOfCollection<TModel>> GetAll(GetRequest<TModel> request,
            CancellationToken cancellationToken, Expression<Func<TEntity, bool>> predicate = null)
            => base.GetAll(request, cancellationToken, predicate);

        public virtual Task<ResultOfCollection<TModel>> HandleGetRequest(GetRequest<TModel> request, 
            CancellationToken cancellationToken, params Expression<Func<TEntity, bool>>[] predicate)
            => base.HandleGetRequest(request, cancellationToken, predicate);

    }
}
