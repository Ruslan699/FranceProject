using System;
using System.Collections.Generic;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.PostalCode
{
    public class GetPostalCodeMapDatas : GetRequest<Models.PostalCodeMapData>, IRequest
    {
        public ICollection<Guid> DepartamentIds { get; }

        public GetPostalCodeMapDatas(ICollection<Guid> departamentIds, bool includeInactive = false) : base(includeInactive)
        {
            DepartamentIds = departamentIds ?? new Guid[0];
        }
    }
}
