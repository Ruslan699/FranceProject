using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using DaddyAgencies.Common.MediatR;
using DaddyAgencies.Common.MediatR.Processors;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;

namespace DaddyAgencies.Application
{
    public class ApplicationCompositionRoot
    {
        public static void Register(Container serviceContainer, Assembly[] assemblies, bool useValidation = true)
        {
            RegisterAutoMapper(serviceContainer, assemblies);
            RegisterMediatrPipeline(serviceContainer, assemblies, useValidation);
        }

        private static void RegisterMediatrPipeline(Container container, Assembly[] assemblies, bool useValidation)
        {
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);

            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(LoggingBehavior<,>),
                typeof(ExceptionHandlingBehavior<,>),
                typeof(RequestPreProcessorBehavior<,>)
            });

            if (useValidation)
                container.Register(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            container.Collection.Register(typeof(IRequestPreProcessor<>),
                new[] { typeof(RequestLanguagePreProcessor<>) });
            container.Collection.Register(typeof(IRequestPostProcessor<,>), Enumerable.Empty<Type>());
            container.RegisterInstance(new ServiceFactory(container.GetInstance));
        }

        private static void RegisterAutoMapper(Container container, IEnumerable<Assembly> assemblies)
        {
            var mapperConfigExp = new MapperConfigurationExpression();
            mapperConfigExp.Advanced.AllowAdditiveTypeMapCreation = true;
            mapperConfigExp.ConstructServicesUsing(container.GetInstance);

            var mappingProfiles = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(Profile).IsAssignableFrom(t))
                .ToArray();
            mapperConfigExp.AddProfiles(mappingProfiles);


            var mapperConfig = new MapperConfiguration(mapperConfigExp);
            mapperConfig.AssertConfigurationIsValid();

            container.RegisterSingleton<IMapper>(() => new Mapper(mapperConfig, container.GetInstance));
        }
    }
}
