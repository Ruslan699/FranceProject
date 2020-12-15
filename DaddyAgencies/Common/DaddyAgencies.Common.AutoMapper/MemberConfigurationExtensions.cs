using System;
using AutoMapper;

namespace DaddyAgencies.Common.AutoMapper
{
    public static class MemberConfigurationExtensions
    {
        public static void DateTimeNow<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> option) =>
            option.MapFrom(src => DateTime.Now);

        public static void DateTimeUtcNow<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> option) =>
            option.MapFrom(src => DateTime.UtcNow);

        public static void NewGuid<TSource, TDestination, TMember>(this IMemberConfigurationExpression<TSource, TDestination, TMember> option) =>
            option.MapFrom(src => Guid.NewGuid());
    }
}
