using System;
using System.Collections.Generic;
using System.Linq;
using DaddyAgencies.Common.Util;

namespace DaddyAgencies.Common.Contracts
{
    public class Issue
    {
        public Issue(IssueOrigin origin, params string[] reasons)
        {
            if (origin == default(IssueOrigin))
                throw new ArgumentException(nameof(origin));
            if (reasons == null)
                throw new ArgumentNullException(nameof(origin));
            if (!reasons.Any())
                throw new ArgumentException(nameof(reasons));

            Origin = origin;
            Reasons = reasons;
        }

        public Issue(IssueOrigin severity, IEnumerable<string> reasons)
            : this(severity, reasons.ToArray())
        {
        }

        public IssueOrigin Origin { get; }

        public IEnumerable<string> Reasons { get; }

        public override string ToString() =>
            $"Issue -> [Origin: {Origin} [Code: {(int)Origin}]\n Reasons: {Reasons.JoinStrings()}]";
    }
}
