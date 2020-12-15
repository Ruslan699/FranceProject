using System;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Inseption
{
    public class SaveInseptionUser : UidRequest<Guid>, IRequest
    {
        public Guid UserUid { get; }

        public SaveInseptionUser(Guid inseptionUid, Guid userUid)
        {
            UserUid = userUid;
            Uid = inseptionUid;
        }
    }
}
