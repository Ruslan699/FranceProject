using DaddyAgencies.Common.Application.Features;
using MediatR;
using System;

namespace DaddyAgencies.Application.Features.Property
{
    public class GetProperties : GetRequest<Models.PropertyPreview>, IRequest
    {
        public GetProperties(bool includeInactive = false) : base(includeInactive)
        {
        }
    }

    public class GetAgentProperties : GetRequest<Models.PropertyPreview>, IRequest
    {
        public Guid UserId { get; }

        public GetAgentProperties(Guid userId, bool includeInactive = false) : base(includeInactive)
        {
            UserId = userId;
        }
    }
    
}
