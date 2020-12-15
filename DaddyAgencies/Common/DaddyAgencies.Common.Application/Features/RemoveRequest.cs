using System;

namespace DaddyAgencies.Common.Application.Features
{
    public class RemoveRequest : UidRequest
    {
        public RemoveRequest() { }

        public RemoveRequest(Guid uid) : base(uid) { }
    }
}
