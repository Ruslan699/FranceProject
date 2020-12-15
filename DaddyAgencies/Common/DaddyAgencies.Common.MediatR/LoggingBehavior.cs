using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using DaddyAgencies.Common.Contracts;
using MediatR;
using Newtonsoft.Json;

namespace DaddyAgencies.Common.MediatR
{
    public class LoggingBehavior<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce>
        where TRequest : Request
        where TResponce : Result
    {
        private readonly ILog _log;

        public LoggingBehavior(ILogManager logManager)
        {
            _log = (logManager ?? throw new ArgumentNullException(nameof(logManager))).GetLogger<TRequest>();
        }

        public async Task<TResponce> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponce> next)
        {
            _log.Info(JsonConvert.SerializeObject(request));
            var response = await next();
            _log.Info(JsonConvert.SerializeObject(response));
            return response;
        }
    }
}
