using System.Collections.Generic;

namespace DaddyAgencies.Common.Contracts
{
    public static class RequestExtensions
    {
        public static Result AsResult(this Request request)
        {
            request.SetCompleted();
            return new Result(request.Id, request.LeadTime);
        }

        public static Result<T> AsResultOf<T>(this Request request, T payload)
        {
            request.SetCompleted();
            return new Result<T>(request.Id, payload, request.LeadTime);
        }
    
        public static ResultOfCollection<T> AsResultOfCollection<T>(this Request request, IEnumerable<T> collection)
        {
            request.SetCompleted();
            return new ResultOfCollection<T>(request.Id, collection, request.LeadTime);
        }
    }
}
