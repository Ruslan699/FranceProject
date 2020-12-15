using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Departament
{
    public class GetDepartamentsView : GetRequest<Models.DepartamentView>, IRequest
    {
    }

    public class GetDepartaments : GetRequest<Models.Departament>, IRequest
    {
        public ICollection<Guid> RegionIds { get; }

        public GetDepartaments()
        {
            RegionIds = new List<Guid>();
        }

        public GetDepartaments(ICollection<Guid> regionIds, bool includeInactive = false) : base(includeInactive)
        {
            RegionIds = regionIds ?? new Guid[0];
        }
    }

    public class GetDepartamentMapDatas : GetRequest<Models.DepartamentMapData>, IRequest
    {
        public ICollection<Guid> RegionIds { get; }

        public GetDepartamentMapDatas(ICollection<Guid> regionIds, bool includeInactive = false) : base(includeInactive)
        {
            RegionIds = regionIds ?? new Guid[0];
        }
    }
}
