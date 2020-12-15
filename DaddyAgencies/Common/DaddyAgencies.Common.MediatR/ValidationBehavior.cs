using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DaddyAgencies.Common.Contracts;
using FluentValidation;
using MediatR;

namespace DaddyAgencies.Common.MediatR
{

    public class ValidationBehavior<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce>
        where TRequest : Request
        where TResponce : Result
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidationBehavior(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

        public Task<TResponce> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponce> next)
        {
            var requestValidator = _validatorFactory.GetValidator<TRequest>();
            if (requestValidator == null)
                return next();

            var validationResult = requestValidator.Validate(request);
            if (validationResult.IsValid)
                return next();

            var reasons = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            var issue = new Issue(IssueOrigin.UserInput, reasons);
            request.SetCompleted();
            var failedResult = Activator.CreateInstance(typeof(TResponce), request.Id, issue, request.LeadTime) as TResponce;

            return Task.FromResult(failedResult);
        }
    }
}
