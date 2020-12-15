using DaddyAgencies.Common.Contracts;

namespace DaddyAgencies.Common.Application.Features
{
    public class GetRequest<T> : RequestOfCollection<T>
    {
        public GetRequest(bool includeInactive = false)
        {
            IcludeInactivcve = includeInactive;
        }

        public bool IcludeInactivcve { get; set; }
    }
}
