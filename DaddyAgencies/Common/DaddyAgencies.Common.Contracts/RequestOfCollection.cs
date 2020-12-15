using MediatR;

namespace DaddyAgencies.Common.Contracts
{
    public abstract class RequestOfCollection<T> : Request, IRequest<ResultOfCollection<T>>
    {
    }
}
