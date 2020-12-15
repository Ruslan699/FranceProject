using System.Threading;
using System.Threading.Tasks;
using DaddyAgencies.Common.Contracts;
using MediatR.Pipeline;

namespace DaddyAgencies.Common.MediatR.Processors
{
    public class RequestLanguagePreProcessor<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : Request
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var languageAwareRequest = request as ILanguageAwareRequest;
            languageAwareRequest.Language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            return Task.CompletedTask;
        }
    }
}
