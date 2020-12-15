

using System;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Contracts;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class ProcessInseption : Request
    {
        public Guid InseptionUid { get; set; }
        public InseptionStatus NewStatus { get; set; }
        public DateTime ConfirmedDate { get; set; }

        public ProcessInseption()
        {
            
        }

        public ProcessInseption(Guid inseptionUid, InseptionStatus newStatus)
        {
            InseptionUid = inseptionUid;
            NewStatus = newStatus;
        }
    }
}
