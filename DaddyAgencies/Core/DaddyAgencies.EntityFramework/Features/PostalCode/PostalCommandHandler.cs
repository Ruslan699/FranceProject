using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.EntityFramework.Features.PostalCode
{
    using ApplicationFeatures = Application.Features.PostalCode;
    using ApplicationModels = Application.Models;
    using Models;
    using DaddyAgencies.Common.Util;
    using System.Collections.Generic;

    public class PostalCommandHandler : RequestHandler<ApplicationModels.PostalCode, PostalCodeEntity>,
        IRequestHandler<ApplicationFeatures.GetPostalCodes, ResultOfCollection<ApplicationModels.PostalCode>>,
        IRequestHandler<ApplicationFeatures.GetPostalCodesView, ResultOfCollection<ApplicationModels.PostalCodeView>>,
        IRequestHandler<ApplicationFeatures.PostalCodeRegion, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.GetPostalCodeMapDatas, ResultOfCollection<ApplicationModels.PostalCodeMapData>>,
        IRequestHandler<ApplicationFeatures.SavePostalCode, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.FindPostalCode, Result<ApplicationModels.PostalCode>>,
        IRequestHandler<ApplicationFeatures.DeletePostalCode, Result>,
        IRequestHandler<ApplicationFeatures.GetUserPostalCodes, ResultOfCollection<Guid>>
    {
        public PostalCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<ResultOfCollection<ApplicationModels.PostalCode>> Handle(ApplicationFeatures.GetPostalCodes request,
            CancellationToken cancellationToken) => HandleGetRequest(request, cancellationToken, 
            query => query.Where(x => !request.DepartamentIds.Any() || request.DepartamentIds.Contains(x.DepartamentUid)).OrderBy(x => x.Name));

        public Task<ResultOfCollection<ApplicationModels.PostalCodeMapData>> Handle(ApplicationFeatures.GetPostalCodeMapDatas request,
            CancellationToken cancellationToken) => HandleGetRequest(request, cancellationToken,
            query => query.Where(x => !request.DepartamentIds.Any() || request.DepartamentIds.Contains(x.DepartamentUid)).OrderBy(x => x.Name));

        public async Task<Result<Guid>> Handle(ApplicationFeatures.PostalCodeRegion request,
            CancellationToken cancellationToken)
        {
            Guid regionUid;
            using (var db = new ProjectDbContext())
            {
                regionUid = await db.PostalCodeEntities.Where(x => x.Uid == request.Uid).Select(x => x.Departament.RegionUid)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            return request.AsResultOf(regionUid);
        }

        public Task<Result<Guid>> Handle(ApplicationFeatures.SavePostalCode request,
            CancellationToken cancellationToken) => HandleSaveRequest(request, cancellationToken);

        public Task<Result<ApplicationModels.PostalCode>> Handle(ApplicationFeatures.FindPostalCode request,
            CancellationToken cancellationToken) => HandleFindRequest(request, cancellationToken);

        public Task<Result> Handle(ApplicationFeatures.DeletePostalCode request, CancellationToken cancellationToken) =>
            HandleDeleteRequest(request, cancellationToken);

        public Task<ResultOfCollection<ApplicationModels.PostalCodeView>> Handle(
            ApplicationFeatures.GetPostalCodesView request, CancellationToken cancellationToken) =>
            HandleGetRequest(request, cancellationToken);

        public async Task<ResultOfCollection<Guid>> Handle(ApplicationFeatures.GetUserPostalCodes request, CancellationToken cancellationToken)
        {
            List<Guid> regionUid;
            using (var db = new ProjectDbContext())
            {
                regionUid = await db.UserPostalCodeEntities.Where(x => x.UserUid == request.UserUid)
                    .Select(x => x.PostalCodeUid).ToListAsync(cancellationToken);
            }
            return request.AsResultOfCollection(regionUid);
        }
    }
}
