using System;
using System.Collections.Generic;
using System.Linq;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class GetRequestedInseption : GetRequest<Models.Inseption>, IRequest
    {
        public Guid UserUid { get; }

        public GetRequestedInseption(Guid userUid)
        {
            UserUid = userUid;
        }
    }

    public class GetUserInseptions : UidRequest<IQueryable<Models.Inseption>>, IRequest
    {
        public Guid UserUid { get; }

        public GetUserInseptions()
        {

        }

        public GetUserInseptions(Guid userUid)
        {
            UserUid = userUid;
        }
    }

    public class GetUserVendorInseptions : UidRequest<IQueryable<Models.Inseption>>, IRequest
    {
        public Guid UserUid { get; }

        public GetUserVendorInseptions()
        {

        }

        public GetUserVendorInseptions(Guid userUid)
        {
            UserUid = userUid;
        }
    }
}
