using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.Util;
using MediatR;

namespace DaddyAgencies.EntityFramework.Features.Property
{
    using ApplicationFeatures = Application.Features.Property;
    using ApplicationModels = Application.Models;
    using Models;

    public class PropertyCommandHandler : RequestHandler<ApplicationModels.PropertyPreview, PropertyEntity>,
        IRequestHandler<ApplicationFeatures.FindProperty, Result<ApplicationModels.PropertyPreview>>,
        IRequestHandler<ApplicationFeatures.SaveProperty, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.GetProperties, ResultOfCollection<ApplicationModels.PropertyPreview>>,
        IRequestHandler<ApplicationFeatures.GetPropertiesByFilter, ResultOfCollection<ApplicationModels.PropertyPreview>>,
        IRequestHandler<ApplicationFeatures.GetPropertyCountByFilter, Result<int>>,
        IRequestHandler<ApplicationFeatures.DeleteProperty, Result>,
        IRequestHandler<ApplicationFeatures.GetAgentProperties, ResultOfCollection<ApplicationModels.PropertyPreview>>,
        IRequestHandler<ApplicationFeatures.UpdatePropertyDocumentRowNo, Result<Guid>>

    {
        public PropertyCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<ResultOfCollection<ApplicationModels.PropertyPreview>> Handle(ApplicationFeatures.GetProperties request,
            CancellationToken cancellationToken) => HandleGetRequest(request, cancellationToken);

        public Task<ResultOfCollection<ApplicationModels.PropertyPreview>> Handle(ApplicationFeatures.GetPropertiesByFilter request, CancellationToken cancellationToken)
        {
            var filters = GetFilers(request.Filter);
            return HandleGetRequest(request, cancellationToken, query =>
            {
                query = filters.Aggregate(query, (current, expression) => current.Where(expression));
                if (request.Filter.Sortby.HasValue)
                {
                    switch (request.Filter.Sortby)
                    {
                        case 1:
                            query = query.OrderByDescending(x => x.CreatedUtc);
                            break;
                        case 2:
                            query = query.OrderBy(x => x.TotalSquare);
                            break;
                        case 3:
                            query = query.OrderBy(x => x.SalePrice);
                            break;
                        case 4:
                            query = query.OrderByDescending(x => x.SalePrice);
                            break;
                        default:
                            query = query.OrderByDescending(x => x.CreatedUtc);
                            break;
                    }
                }
                else
                    query = query.OrderByDescending(x => x.CreatedUtc);
                query = query.Skip(request.Filter.Skip).Take(request.Filter.Take);
                return query;
            });
        }

        public Task<Result<ApplicationModels.PropertyPreview>> Handle(ApplicationFeatures.FindProperty request,
            CancellationToken cancellationToken) => HandleFindRequest(request, cancellationToken);

        public async Task<Result<Guid>> Handle(ApplicationFeatures.SaveProperty request,
            CancellationToken cancellationToken)
        {
            PropertyEntity dbEntity;
            using (var db = new ProjectDbContext())
            {
                if (request.Uid == default(Guid))
                {
                    int count = 1;
                    dbEntity = Mapper.Map<PropertyEntity>(request);
                    dbEntity.Documents = new List<PropertyDocumentEntity>();
                    foreach (var file in request.Files)
                    {
                        var dbfile = Mapper.Map<PropertyDocumentEntity>(file);
                        dbfile.RowNo = count++;
                        dbEntity.Documents.Add(dbfile);
                    }
                    db.Set<PropertyEntity>().Add(dbEntity);
                }
                else
                {
                    var result = db.PropertyDocumentEntities.Where(x => x.PropertyUid == request.Uid).Count();
                    int count = 0;
                    if (result > 0)
                    {
                        count = (int)db.PropertyDocumentEntities.Where(x => x.PropertyUid == request.Uid).OrderByDescending(x => x.RowNo).FirstOrDefault().RowNo;
                    }
                    
                    dbEntity = await db.Set<PropertyEntity>().Include(x => x.Documents).FirstAsync(x => x.Uid == request.Uid, cancellationToken);
                    //var originalDocs = new List<DocumentBaseEntity>(dbEntity.Documents);
                    //foreach (var propertyDocumentEntity in originalDocs)
                    //{
                    //    db.Entry(propertyDocumentEntity).State = EntityState.Deleted;
                    //}

                    foreach (var file in request.Files)
                    {
                        var dbfile = Mapper.Map<PropertyDocumentEntity>(file);
                        dbfile.RowNo = ++count;
                        dbEntity.Documents.Add(dbfile);
                    }
                    Mapper.Map(request, dbEntity);
                }
                await db.SaveChangesAsync(cancellationToken);
            }

            return request.AsResultOf(dbEntity.Uid);
        }

        public Task<Result<int>> Handle(ApplicationFeatures.GetPropertyCountByFilter request, CancellationToken cancellationToken)
        {
            var filters = GetFilers(request.Filter);
            return HandleCountRequest<PropertyEntity>(request, cancellationToken, query =>
            {
                query = filters.Aggregate(query, (current, expression) => current.Where(expression));
                if (request.Filter.Sortby.HasValue)
                {
                    switch (request.Filter.Sortby)
                    {
                        case 1:
                            query = query.OrderByDescending(x => x.CreatedUtc);
                            break;
                        case 2:
                            query = query.OrderBy(x => x.TotalSquare);
                            break;
                        case 3:
                            query = query.OrderBy(x => x.SalePrice);
                            break;
                        case 4:
                            query = query.OrderByDescending(x => x.SalePrice);
                            break;
                        default:
                            query = query.OrderByDescending(x => x.CreatedUtc);
                            break;
                    }
                }
                else
                    query = query.OrderByDescending(x => x.CreatedUtc);
                return query;
            });
        }


        private List<Expression<Func<PropertyEntity, bool>>> GetFilers(ApplicationModels.PropertyFilter filter)
        {
            var filters = new List<Expression<Func<PropertyEntity, bool>>>();
            if (filter != null)
            {
                if (filter.PostalCodes != null && filter.PostalCodes.Any(x => x != default(Guid)))
                {
                    filters.Add(prop => filter.PostalCodes.Where(x => x != default(Guid)).Contains(prop.PostalCodeUid.Value));
                }
                else if (filter.Departaments != null && filter.Departaments.Any(x => x != default(Guid)))
                {
                    filters.Add(prop => filter.Departaments.Where(x => x != default(Guid)).Contains(prop.PostalCode.DepartamentUid));
                }
                else if (filter.Regions != null && filter.Regions.Any(x => x != default(Guid)))
                {
                    filters.Add(prop => filter.Regions.Where(x => x != default(Guid)).Contains(prop.PostalCode.Departament.RegionUid));
                }

                if (filter.PriceMin > 0)
                    filters.Add(prop => prop.SalePrice >= filter.PriceMin);
                if (filter.PriceMax > 0)
                    filters.Add(prop => prop.SalePrice <= filter.PriceMax);

                if (filter.SquareMin > 0)
                    filters.Add(prop => prop.TotalSquare >= filter.SquareMin);
                if (filter.SquareMax > 0)
                    filters.Add(prop => prop.TotalSquare <= filter.SquareMax);

                if (filter.RoomsMin > 0)
                    filters.Add(prop => prop.RoomsCount >= filter.RoomsMin);
                if (filter.RoomsMax > 0)
                    filters.Add(prop => prop.RoomsCount <= filter.RoomsMax);

                if (filter.Parking != null)
                    filters.Add(prop => prop.Parking.Contains(filter.Parking));
                if (filter.EnergyClass != null)
                    filters.Add(prop => prop.EnergyClass.Contains(filter.EnergyClass));
                if (filter.Ges != null)
                    filters.Add(prop => prop.Ges.Contains(filter.Ges));

                if (filter.PropertyTypes.Any())
                    filters.Add(prop => filter.PropertyTypes.Select(x => (int)x).Contains(prop.PropertyType));

                if (!string.IsNullOrWhiteSpace(filter.Keyword))
                    filters.Add(prop => prop.Name.Contains(filter.Keyword)
                                        || prop.Description.Contains(filter.Keyword));

              
            }
            return filters;
        }

        public Task<Result> Handle(ApplicationFeatures.DeleteProperty request, CancellationToken cancellationToken) =>
            HandleDeleteRequest(request, cancellationToken);

        public async Task<ResultOfCollection<ApplicationModels.PropertyPreview>> Handle(ApplicationFeatures.GetAgentProperties request, CancellationToken cancellationToken)
        {
            ResultOfCollection<ApplicationModels.PropertyPreview> models;
            using (var db = new ProjectDbContext())
            {
                var queryable = db.Set<PropertyEntity>()
                    .Join(db.UserPostalCodeEntities.Where(x => x.UserUid == request.UserId), r => r.PostalCodeUid, ro => ro.PostalCodeUid, (r, ro) => r)
                    .AsNoTracking()
                    .Where(c => request.IcludeInactivcve || !c.IsDeleted);

                models = await queryable.ProjectToListAsync<ApplicationModels.PropertyPreview>(Mapper.ConfigurationProvider)
                    .Map(request.AsResultOfCollection);
            }

            return models;
        }

        public async Task<Result<Guid>> Handle(ApplicationFeatures.UpdatePropertyDocumentRowNo request, CancellationToken cancellationToken)
        {
            PropertyDocumentEntity dbEntity;
            using (var db = new ProjectDbContext())
            {
                int count = 1;
                foreach (Guid propertUid in request.PropertyUids)
                {
                    var item = db.PropertyDocumentEntities.Where(x => x.Uid == propertUid).FirstOrDefault();
                    item.RowNo = count;
                    db.Entry(item).State = EntityState.Modified;
                    count++;
                    await db.SaveChangesAsync(cancellationToken);
                }
            }
            return null;
        }
    }
}
