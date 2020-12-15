using System;
using MediatR;

namespace DaddyAgencies.Common.Contracts
{
    public abstract class Request : IRequest<Result>, ILanguageAwareRequest
    {
        protected Request()
        {
            Id = Guid.NewGuid();
            Initiated = DateTime.UtcNow;
        }

        internal Request(Guid initiatorId) : this()
        {
            if (initiatorId == default(Guid))
                throw new ArgumentException(nameof(initiatorId));
            InitiatorId = initiatorId;
        }

        public Guid Id { get; }

        public Guid InitiatorId { get; internal set; }

        public string Language { get; private set; }

        public DateTime Initiated { get;  }

        public DateTime? Completed { get; private set; }

        public TimeSpan LeadTime => (Completed ?? DateTime.UtcNow) - Initiated;

        public void SetCompleted() => Completed = DateTime.UtcNow;

        string ILanguageAwareRequest.Language { set => Language = value; }
    }

    public abstract class Request<T> : Request, IRequest<Result<T>>
    {
    }
}
