using System;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class SaveInseption : SaveRequest, IRequest
    {
        public Guid? UserUid { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string RequestDate { get; set; }
        public Guid? PostalCodeUid { get; set; }
        public Guid? PropertyUid { get; set; }
        public Guid RegionUid { get; set; }
        public string Description { get; set; }
        public InseptionStatus Status { get; set; }
    }
}
