using System;

namespace DaddyAgencies.Common.Application.Features
{
    public class DeleteRequest : UidRequest
    {
        public DeleteRequest() { }

        public DeleteRequest(Guid uid) : base(uid) { }
    }
}
