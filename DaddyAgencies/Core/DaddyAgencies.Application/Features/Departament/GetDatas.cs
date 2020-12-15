using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Departament
{
    public class GetDatas : GetRequest<Models.Departament>, IRequest
    {
        public ICollection<Guid> RegionIds { get; }

        public GetDatas()
        {
            RegionIds = new List<Guid>();
        }

        public GetDatas(ICollection<Guid> regionIds, bool includeInactive = false) : base(includeInactive)
        {
            RegionIds = regionIds ?? new Guid[0];
        }

        public class GetDataMapDatas : GetRequest<Models.PostalCodeMapData>, IRequest
        {
            public ICollection<Guid> RegionIds { get; }

            public GetDataMapDatas(ICollection<Guid> regionIds, bool includeInactive = false) : base(includeInactive)
            {
                RegionIds = regionIds ?? new Guid[0];
            }
        }
    }
}
