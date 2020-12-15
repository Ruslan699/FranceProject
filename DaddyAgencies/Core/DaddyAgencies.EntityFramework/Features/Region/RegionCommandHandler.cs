using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.EntityFramework.Features.Region
{
    using ApplicationFeatures = Application.Features.Region;
    using ApplicationModels = Application.Models;
    using Models;
    using RegionFeatures = DaddyAgencies.Application.Features.Region;
    using DaddyAgencies.Common.Util;
    using System;

    public class RegionCommandHandler : RequestHandler<ApplicationModels.Region, RegionEntity>,
        IRequestHandler<ApplicationFeatures.GetRegions, ResultOfCollection<ApplicationModels.Region>>,
        IRequestHandler<ApplicationFeatures.GetRegionMapDatas, ResultOfCollection<ApplicationModels.RegionMapData>>,
        IRequestHandler<ApplicationFeatures.SaveRegion, Result<Guid>>,
        IRequestHandler<ApplicationFeatures.DeleteRegion, Result>,
        IRequestHandler<RegionFeatures.FindRegion, Result<ApplicationModels.Region>>,
        IRequestHandler<RegionFeatures.FindRegionMapData, Result<ApplicationModels.RegionMapData>>
    {
        public RegionCommandHandler(IMapper mapper) : base(mapper)
        {
        }

        public Task<ResultOfCollection<ApplicationModels.Region>> Handle(ApplicationFeatures.GetRegions request,
            CancellationToken cancellationToken) => HandleGetRequest(request, cancellationToken, query => query.OrderBy(x => x.Name));

        public Task<ResultOfCollection<ApplicationModels.RegionMapData>> Handle(ApplicationFeatures.GetRegionMapDatas request, 
            CancellationToken cancellationToken)
            => HandleGetRequest(request, cancellationToken, query => query.OrderBy(x => x.Name));

        public Task<Result<ApplicationModels.Region>> Handle(RegionFeatures.FindRegion request,
            CancellationToken cancellationToken) => HandleFindRequest(request, cancellationToken);

        public Task<Result<Guid>> Handle(ApplicationFeatures.SaveRegion request, CancellationToken cancellationToken) =>
            HandleSaveRequest(request, cancellationToken);

        public Task<Result> Handle(ApplicationFeatures.DeleteRegion request, CancellationToken cancellationToken) =>
             HandleDeleteRequest(request, cancellationToken);

        public Task<Result<ApplicationModels.RegionMapData>> Handle(ApplicationFeatures.FindRegionMapData request, 
            CancellationToken cancellationToken) =>
            HandleFindByRequest(request, r => r.Uid == request.Uid, cancellationToken);
    }
}
