using System;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Property
{
    public class FindProperty : FindRequest<Models.PropertyPreview>, IRequest
    {
        public FindProperty(Guid uid) : base(uid)
        {
            
        }
    }
}
