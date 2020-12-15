using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DaddyAgencies.Common.Contracts
{
    public class ResultOfCollection<T> : Result, IEnumerable<T>
    {
        public ResultOfCollection(Guid requestId, IEnumerable<T> collection, TimeSpan leadTime)
            : base(requestId, leadTime)
        {
            Collection = (collection ?? throw new ArgumentNullException(nameof(collection))).ToArray();
        }

        public ResultOfCollection(Guid requestId, Issue issue, TimeSpan leadTime)
            : base(requestId, issue, leadTime)
        {
        }

        #region IEnumerable

        public IEnumerable<T> Collection { get; } = Enumerable.Empty<T>();

        public IEnumerator<T> GetEnumerator() =>
            Collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            Collection.GetEnumerator();

        #endregion
    }
}
