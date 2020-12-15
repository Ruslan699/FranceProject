using System;

namespace DaddyAgencies.Common.Application.Features
{
    public class FindRequest<T> : UidRequest<T>
    {
        public FindRequest() { }

        public FindRequest(Guid uid) : base(uid) { }
    }
}
