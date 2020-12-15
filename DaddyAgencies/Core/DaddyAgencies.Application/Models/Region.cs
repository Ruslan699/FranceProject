using System;
using System.Collections.Generic;

namespace DaddyAgencies.Application.Models
{
    public class Region
    {
        public Guid? Uid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Departament> Departaments { get; set; }
    }

    public class RegionMapData : Region
    {
        public string PathString { get; set; }

        public string MapBounds { get; set; }

        public string MapCenter { get; set; }

        public string MapZoom { get; set; }
    }
}
