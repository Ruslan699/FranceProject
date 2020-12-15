using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.Util;
using DaddyAgencies.EntityFramework.Models;
using MediatR;

namespace DaddyAgencies.EntityFramework.Features.Inseption
{
    using ApplicationFeatures = Application.Features.Inseption;
    using ApplicationModels = Application.Models;

    public class InseptionCommandHandler : RequestHandler<ApplicationModels.Inseption, InseptionEntity>,
        IRequestHandler<ApplicationFeatures.SaveInseption, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.SaveInseptionUser, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.GetAvailableInseptionDates, ResultOfCollection<KeyValuePair<DateTime, List<int>>>>,
        IRequestHandler<ApplicationFeatures.GetRequestedInseption, ResultOfCollection<ApplicationModels.Inseption>>,
        IRequestHandler<ApplicationFeatures.GetUserInseptions, Result<IQueryable<ApplicationModels.Inseption>>>,
        IRequestHandler<ApplicationFeatures.GetUserVendorInseptions, Result<IQueryable<ApplicationModels.Inseption>>>,
        IRequestHandler<ApplicationFeatures.FindInseption, Result<ApplicationModels.Inseption>>,
        IRequestHandler<ApplicationFeatures.ProcessInseption, Result>,
        IRequestHandler<ApplicationFeatures.GetRegions, Result<IQueryable<ApplicationModels.Region>>>,
        IRequestHandler<ApplicationFeatures.GetAgentsEmailsForProperty, ResultOfCollection<ApplicationModels.PropertyAgentInfo>>,
        IRequestHandler<ApplicationFeatures.DeleteInseption, Result>
    {

        public InseptionCommandHandler(IMapper mapper) : base(mapper)
        {
        }


        public Task<Result<IQueryable<ApplicationModels.Region>>> Handle(ApplicationFeatures.GetRegions request, CancellationToken cancellationToken)
        {
            var ctx = new ProjectDbContext();
            var regions = ctx.RegionEntities.Where(c => request.IncludeInactive || !c.IsDeleted)
                .ProjectTo<ApplicationModels.Region>(Mapper.ConfigurationProvider);
            return Task.FromResult(request.AsResultOf(regions));
        }

        public Task<Result<Guid>> Handle(ApplicationFeatures.SaveInseption request, CancellationToken cancellationToken)
            => HandleSaveRequest(request, cancellationToken);

        public Task<ResultOfCollection<ApplicationModels.Inseption>> Handle(
            ApplicationFeatures.GetRequestedInseption request, CancellationToken cancellationToken) =>
            HandleGetRequest(request, cancellationToken, predicate: insp => insp.RequesterUserUid == request.UserUid);

        public async Task<ResultOfCollection<KeyValuePair<DateTime, List<int>>>> Handle(ApplicationFeatures.GetAvailableInseptionDates request, CancellationToken cancellationToken)
        {
            InseptionEntity[] currentInseptions;
            using (var db = new ProjectDbContext())
            {
                var query = db.InseptionEntities
                    .AsNoTracking()
                    .Where(x => x.PropertyUid == request.PropertyUid);
                currentInseptions = await query.ToArrayAsync(cancellationToken);
            }
            currentInseptions.Each(x => x.ConfirmedDateUtc = x.ConfirmedDateUtc ?? x.RequestedDateUtc);
            var result = currentInseptions.GroupBy(x => x.ConfirmedDateUtc.Value.Date)
                .Select(x =>
                    new KeyValuePair<DateTime, List<int>>(x.Key, x.Select(dt => dt.ConfirmedDateUtc.Value.Hour).ToList()))
                .ToList();
            return request.AsResultOfCollection(result);
        }

        public async Task<Result<Guid>> Handle(ApplicationFeatures.SaveInseptionUser request, CancellationToken cancellationToken)
        {
            using (var db = new ProjectDbContext())
            {
                var inseption = await db.InseptionEntities
                    .Where(x => x.Uid == request.Uid)
                    .FirstOrDefaultAsync(cancellationToken);
                if (inseption != null)
                {
                    inseption.RequesterUserUid = request.UserUid;
                    await db.SaveChangesAsync(cancellationToken);
                }
            }
            return request.AsResultOf(request.Uid);
        }

        public async Task<Result<IQueryable<ApplicationModels.Inseption>>> Handle(ApplicationFeatures.GetUserInseptions request, CancellationToken cancellationToken)
        {
            List<ApplicationModels.Inseption> inseptions;
            if (request.UserUid != default(Guid))
            {
                using (var ctx = new ProjectDbContext())
                {
                    inseptions = await ctx.InseptionEntities.Where(x => !x.IsDeleted && x.PropertyUid != null).OrderBy(x=>x.Status).Join(ctx.UserPostalCodeEntities.Where(x => x.UserUid == request.UserUid),
                    x => x.PostalCodeUid,
                    x => x.PostalCodeUid,
                    (ins, upc) => ins).ProjectToListAsync<ApplicationModels.Inseption>(Mapper.ConfigurationProvider);
                }
            }
            else
            {
                using (var ctx = new ProjectDbContext())
                {
                    inseptions = await ctx.InseptionEntities.Where(x => !x.IsDeleted && x.PropertyUid!=null).OrderBy(x => x.Status).ThenBy(x=>x.RequestedDateUtc).ProjectToListAsync<ApplicationModels.Inseption>(Mapper.ConfigurationProvider);
                }
            }

            return request.AsResultOf(inseptions.AsQueryable());
        }

        public async Task<Result<IQueryable<ApplicationModels.Inseption>>> Handle(ApplicationFeatures.GetUserVendorInseptions request, CancellationToken cancellationToken)
        {
            List<ApplicationModels.Inseption> inseptions;
            if (request.UserUid != default(Guid))
            {
                using (var ctx = new ProjectDbContext())
                {
                    inseptions = await ctx.InseptionEntities.Where(x => !x.IsDeleted && x.PropertyUid == null).OrderBy(x => x.Status).Join(ctx.UserPostalCodeEntities.Where(x => x.UserUid == request.UserUid),
                    x => x.PostalCodeUid,
                    x => x.PostalCodeUid,
                    (ins, upc) => ins).ProjectToListAsync<ApplicationModels.Inseption>(Mapper.ConfigurationProvider);
                }
            }
            else
            {
                using (var ctx = new ProjectDbContext())
                {
                    inseptions = await ctx.InseptionEntities.Where(x => !x.IsDeleted && x.PropertyUid == null).OrderBy(x => x.Status).ThenBy(x => x.RequestedDateUtc).ProjectToListAsync<ApplicationModels.Inseption>(Mapper.ConfigurationProvider);
                }
            }

            return request.AsResultOf(inseptions.AsQueryable());
        }

        public Task<Result<ApplicationModels.Inseption>> Handle(ApplicationFeatures.FindInseption request, CancellationToken cancellationToken)
        {
            using (var ctx = new ProjectDbContext())
            {
                var inseption = ctx.InseptionEntities.FirstOrDefault(e => e.Uid == request.Uid);
                var dtoInseption = Mapper.Map<ApplicationModels.Inseption>(inseption);
                return Task.FromResult(request.AsResultOf(dtoInseption));
            }
        }

        public async Task<Result> Handle(ApplicationFeatures.ProcessInseption request, CancellationToken cancellationToken)
        {
            using (var ctx = new ProjectDbContext())
            {
                var inseption = await ctx.InseptionEntities
                    .FirstOrDefaultAsync(e => e.Uid == request.InseptionUid, cancellationToken);
                if (inseption != null)
                {
                    inseption.Status = (int)request.NewStatus;
                    inseption.ConfirmedDateUtc = request.ConfirmedDate;
                    await ctx.SaveChangesAsync(cancellationToken);
                }
            }
            return request.AsResult();
        }

        public async Task<ResultOfCollection<ApplicationModels.PropertyAgentInfo>> Handle(ApplicationFeatures.GetAgentsEmailsForProperty request, CancellationToken cancellationToken)
        {
            var adminEmail = ConfigurationManager.AppSettings["AdminEmail"];

            var defaultEmails = new List<ApplicationModels.PropertyAgentInfo> { new ApplicationModels.PropertyAgentInfo { UserEmail = adminEmail } };

                using (var ctx = new ProjectDbContext())
                {
                    var emails = new List<ApplicationModels.PropertyAgentInfo>();

                    List<PropertyAgents> agents;

                    if (request.PropertyUid.HasValue)
                    {
                        var propertyUidParam = new SqlParameter("@propertyUidParam", request.PropertyUid.Value);
                        agents = await ctx.Database.SqlQuery<PropertyAgents>(
                            "SELECT [PropertyUid], [PropertyName], [UserEmail], [UserUid] " +
                            "FROM [WebUserEmails] " +
                            "WHERE [PropertyUid] = @propertyUidParam", propertyUidParam).ToListAsync(cancellationToken);
                    }
                    else if (request.PostalCodeUid.HasValue)
                    {
                        var postalCodeUidParam = new SqlParameter("@postalCodeUidParam", request.PostalCodeUid.Value);

                        agents = await ctx.Database.SqlQuery<PropertyAgents>(
                                "SELECT TOP(1) [PropertyUid], [PropertyName], [UserEmail], [UserUid] " +
                                "FROM [WebUserEmails] " +
                                "WHERE [PostalCodeUid] = @postalCodeUidParam", postalCodeUidParam)
                            .ToListAsync(cancellationToken);
                    }
                    else
                        agents = new List<PropertyAgents>();

                    if (agents.Any())
                    {
                        foreach (var agent in agents)
                        {
                            emails.Add(new ApplicationModels.PropertyAgentInfo
                            {
                                PropertyUid = agent.PropertyUid,
                                UserEmail = agent.UserEmail,
                                UserUid = agent.UserUid,
                                PropertyName = agent.PropertyName
                            });
                        }
                        return request.AsResultOfCollection(emails.Any() ? emails : defaultEmails);
                    }
                }

            return request.AsResultOfCollection(defaultEmails);
        }

        public Task<Result> Handle(ApplicationFeatures.DeleteInseption request, CancellationToken cancellationToken) =>
            HandleDeleteRequest(request, cancellationToken);
    }
}
