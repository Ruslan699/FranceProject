using System;
using FluentValidation;

namespace DaddyAgencies.Common.SimpleInjector
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public override IValidator CreateInstance(Type validatorType) =>
            _serviceProvider.GetService(validatorType) as IValidator;
    }
}
