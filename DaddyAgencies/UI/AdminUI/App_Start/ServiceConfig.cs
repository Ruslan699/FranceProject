using AutoMapper;
using Common.Logging;
using DaddyAgencies.Application;
using DaddyAgencies.Common.SimpleInjector;
using FluentValidation;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AdminUI.App_Start
{
    public class ServiceConfig
    {
        public static readonly Container Container = new Container();
        private static IEnumerable<Assembly> Assemblies =>
            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("DaddyAgencies") || a.FullName.StartsWith("AdminUI"));

        public static void Register()
        {
            AppDomain.CurrentDomain.Load("DaddyAgencies.EntityFramework");
            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            Container.RegisterMvcIntegratedFilterProvider();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
            RegisterLogging(Container);
            RegisterValidators(Container);
            ApplicationCompositionRoot.Register(Container, Assemblies.ToArray());
            Container.Verify();
        }

        private static void RegisterLogging(Container container)
        {
            container.Register<ILogManager, LogManager>();
        }

        private static void RegisterValidators(Container container)
        {
            container.Collection.Register(typeof(IValidator), Assemblies);
            container.Collection.Register(typeof(IValidator<>), Assemblies);
            container.RegisterSingleton<IValidatorFactory>(() => new ValidatorFactory(container));
        }
    }
}