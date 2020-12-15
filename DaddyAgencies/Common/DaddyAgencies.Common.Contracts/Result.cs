using System;

namespace DaddyAgencies.Common.Contracts
{

    public class Result
    {
        public Result(Guid requestId, TimeSpan leadTime)
        {
            if (requestId == default(Guid))
                throw new ArgumentException(nameof(requestId));
            RequestId = requestId;
            LeadTime = leadTime;
        }

        public Result(Guid requestId, Issue issue, TimeSpan leadTime)
            : this(requestId, leadTime)
        {
            Issue = issue ?? throw new ArgumentNullException();
        }

        public Guid RequestId { get; }

        public TimeSpan LeadTime { get; }

        public Issue Issue { get; }

        public bool IsFailure => Issue != null;

        public bool IsSuccess => Issue == null;
    }

    public class Result<T> : Result
    {
        public Result(Guid requestId, TimeSpan leadTime)
            : base(requestId, leadTime)
        {
            Payload = default(T);
        }

        public Result(Guid requestId, T payload, TimeSpan leadTime)
            : base(requestId, leadTime)
        {
            Payload = payload;
        }

        public Result(Guid requestId, Issue issue, TimeSpan leadTime)
            : base(requestId, issue, leadTime)
        {
        }

        public T Payload { get; }
    }
}
