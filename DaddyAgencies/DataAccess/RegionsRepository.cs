using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Context;
using DataModel.Models;
using DataModel.Repositories;

namespace DataAccess
{
   public class RegionsRepository : IRegionsRepository
    {
        public async Task<ICollection<Region>> GetActive()
        {
            List<RegionEntity> entities;
            using (var cnt = new DataContext())
            {
                entities = await cnt.RegionEntities.ToListAsync();
            }
            return Mapper.Map<List<Region>>(entities);
        }
    }
}
