using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;

namespace DaddyAgencies.Application.Features.Region
{
    public class GetRegions : GetRequest<Models.Region>, IRequest
    {
        public GetRegions(bool includeInactive = false) : base(includeInactive)
        {
        }
    }
    public class GetRegionMapDatas : GetRequest<Models.RegionMapData>, IRequest
    {
        public GetRegionMapDatas(bool includeInactive = false) : base(includeInactive)
        {
        }
    }
    public class FindRegionMapData : FindRequest<Models.RegionMapData>, IRequest
    {
        public FindRegionMapData(Guid uid) : base(uid)
        {
        }
    }
}
