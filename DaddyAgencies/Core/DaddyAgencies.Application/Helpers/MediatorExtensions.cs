using System.Threading.Tasks;
using DaddyAgencies.Common.Contracts;
using MediatR;

namespace DaddyAgencies.Application.Helpers
{
    public static class MediatorExtensions
    {
        public static Task<Result> SendRequest(this IMediator mediator, Request request)
        {
            return mediator.Send(request);
        }

        public static Task<Result<T>> SendRequest<T>(this IMediator mediator, Request<T> request)
        {
            return mediator.Send(request as IRequest<Result<T>>);
        }

        public static Task<ResultOfCollection<T>> SendRequest<T>(this IMediator mediator,
            RequestOfCollection<T> request)
        {
            return mediator.Send(request as IRequest<ResultOfCollection<T>>);
        }
    }
}
