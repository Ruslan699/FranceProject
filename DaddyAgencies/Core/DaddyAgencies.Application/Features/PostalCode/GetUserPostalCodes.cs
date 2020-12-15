using System;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.PostalCode
{
    public class GetUserPostalCodes : GetRequest<Guid>, IRequest
    {
        public Guid UserUid { get; }
        public GetUserPostalCodes()
        {
        }

        public GetUserPostalCodes(Guid userUid)
        {
            UserUid = userUid;
        }
    }
}
