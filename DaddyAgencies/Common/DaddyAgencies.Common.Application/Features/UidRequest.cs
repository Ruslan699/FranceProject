using System;
using DaddyAgencies.Common.Contracts;

namespace DaddyAgencies.Common.Application.Features
{
    public class UidRequest : Request
    {
        public UidRequest() { }

        public UidRequest(Guid uid) => Uid = uid;

        public Guid Uid { get; set; }
    }

    public class UidRequest<T> : Request<T>
    {
        public UidRequest() { }

        public UidRequest(Guid uid) => Uid = uid;

        public Guid Uid { get; set; }
    }
}
