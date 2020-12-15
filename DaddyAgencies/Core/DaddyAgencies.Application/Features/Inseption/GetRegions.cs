using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class GetRegions : UidRequest<IQueryable<Models.Region>>, IRequest
    {
        public bool IncludeInactive { get; set; }

        public GetRegions(bool includeInactive = false)
        {
            IncludeInactive = includeInactive;
        }
    }
}
