using System;
using System.Collections.Generic;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.PostalCode
{
    public class GetPostalCodesView : GetRequest<Models.PostalCodeView>, IRequest
    {
    }

    public class GetPostalCodes : GetRequest<Models.PostalCode>, IRequest
    {
        public ICollection<Guid> DepartamentIds { get; }
        public GetPostalCodes()
        {
            DepartamentIds = new List<Guid>();
        }

        public GetPostalCodes(ICollection<Guid> departamentIds, bool includeInactive = false) : base(includeInactive)
        {
            DepartamentIds = departamentIds ?? new Guid[0];
        }
    }
}
