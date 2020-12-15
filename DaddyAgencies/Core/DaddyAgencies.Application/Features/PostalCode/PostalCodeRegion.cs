using System;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.PostalCode
{
    public class PostalCodeRegion : FindRequest<Guid>, IRequest
    {
        public PostalCodeRegion(Guid postalCodeUid) : base(postalCodeUid)
        {

        }
    }
}
