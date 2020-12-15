using System;
using System.Collections.Generic;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class GetAvailableInseptionDates : RequestOfCollection<KeyValuePair<DateTime, List<int>>>, IRequest
    {
        public Guid PropertyUid { get; }

        public GetAvailableInseptionDates(Guid propertyUid)
        {
            PropertyUid = propertyUid;
        }
    }
}
