using System;
using AutoMapper;
using DaddyAgencies.Common.EntityFramework;

namespace DaddyAgencies.Common.AutoMapper
{
    public class BaseEntityAfterMapAction<TSource, TDestination> : IMappingAction<TSource, TDestination>
        where TDestination : BasePersistenceEntity
    {
        public void Process(TSource source, TDestination destination)
        {
            if (destination.Uid == default(Guid))
            {
                destination.Uid = Guid.NewGuid();
                destination.CreatedUtc = DateTime.UtcNow;
                destination.IsDeleted = false;
                destination.ModifiedUtc = default(DateTime?);
            }
            else
            {
                destination.ModifiedUtc = DateTime.UtcNow;
            }
        }
    }
}
