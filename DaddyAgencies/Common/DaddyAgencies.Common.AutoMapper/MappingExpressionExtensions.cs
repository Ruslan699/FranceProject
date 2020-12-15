using System;
using AutoMapper;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.Common.AutoMapper
{
    public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> BaseEntityAfterMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> option)
            where TDestination : BasePersistenceEntity
        {
            return option.AfterMap<BaseEntityAfterMapAction<TSource, TDestination>>();
        }

        public static IMappingExpression<TSource, TDestination> UseBaseAMappingfterMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> option)
        {
            
            return option.AfterMap(((source, destination) =>
                {
                    Mapper.Map(source, destination, typeof(TSource), typeof(TDestination).BaseType);
                }));
        }
    }
}
