using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using DaddyAgencies.Common.Contracts;
using DaddyAgencies.Common.Util;
using MediatR;

namespace DaddyAgencies.Common.MediatR
{

    public class ExceptionHandlingBehavior<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce>
        where TRequest : Request
        where TResponce : Result
    {
        private readonly ILog _log;

        public ExceptionHandlingBehavior(ILogManager logManager)
        {
            _log = (logManager ?? throw new ArgumentNullException(nameof(logManager))).GetLogger<TRequest>();
        }

        public async Task<TResponce> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponce> next)
        {
            try
            {
                return await next().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                var issue = new Issue(IssueOrigin.Unknown, ex.ExceptionMessages().JoinStrings());
                request.SetCompleted();
                var failedResult = Activator.CreateInstance(typeof(TResponce), request.Id, issue, request.LeadTime) as TResponce;

                return failedResult;
            }
        }
    }
}
