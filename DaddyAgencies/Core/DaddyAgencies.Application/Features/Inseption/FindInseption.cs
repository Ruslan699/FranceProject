using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class FindInseption : FindRequest<Models.Inseption>, IRequest
    {
        public Guid InseptionUid { get; set; }
        public InseptionStatus NewStatus { get; set; }

        public FindInseption(Guid uid) : base(uid)
        {
        }

        public FindInseption(Guid inseptionUid, InseptionStatus newStatus)
        {
            InseptionUid = inseptionUid;
            NewStatus = newStatus;
        }
    }
}
