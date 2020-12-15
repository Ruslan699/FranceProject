using System;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.User
{
    public class SaveUserPostalCodes : SaveRequest, IRequest
    {
        public Guid UserId { get; }
        public Guid[] PostalCodes { get; }

        public SaveUserPostalCodes(Guid userId, Guid[] postalCodes)
        {
            UserId = userId;
            PostalCodes = postalCodes;
        }
    }
}
