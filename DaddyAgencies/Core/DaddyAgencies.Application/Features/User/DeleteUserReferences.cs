using System;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.Application.Features.User
{
    public class DeleteUserReferences : Request, IRequest
    {
        public Guid UserId { get; }

        public DeleteUserReferences(Guid userId)
        {
            UserId = userId;
        }
    }
}
