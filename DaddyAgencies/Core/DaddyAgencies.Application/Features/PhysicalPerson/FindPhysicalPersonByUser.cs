using System;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.Application.Features.PhysicalPerson
{
    public class FindPhysicalPersonByUser : Request<Models.PhysicalPerson>, IRequest
    {
        public Guid UserId { get; }

        public FindPhysicalPersonByUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
