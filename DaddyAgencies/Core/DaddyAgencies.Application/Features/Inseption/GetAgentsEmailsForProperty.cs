using System;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class GetAgentsEmailsForProperty : RequestOfCollection<PropertyAgentInfo>, IRequest
    {
        public Guid? PostalCodeUid { get; }

        public Guid? PropertyUid { get; set; }

        public GetAgentsEmailsForProperty(Guid? postalCodeUid, Guid? propertyUid)
        {
            PostalCodeUid = postalCodeUid;
            PropertyUid = propertyUid;
        }
    }
}
