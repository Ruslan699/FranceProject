using System;

namespace DaddyAgencies.Common.Application.Features
{
    public class SaveRequest : UidRequest<Guid>
    {
        public SaveRequest() { }

        public SaveRequest(Guid uid) : base(uid) { }
    }
}
