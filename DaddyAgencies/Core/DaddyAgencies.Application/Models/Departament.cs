using System;
using System.Collections.Generic;

namespace DaddyAgencies.Application.Models
{
    public class Departament
    {
        public Guid? Uid { get; set; }

        public Guid? RegionUid { get; set; }

        public string Name { get; set; }
        public int RowNo { get; set; }
        public string Description { get; set; }
        public Region Region { get; set; }
        public List<PostalCode> PostalCodes { get; set; }
    }

    public class DepartamentView
    {
        public Guid? Uid { get; set; }

        public Guid? RegionUid { get; set; }

        public string Name { get; set; }

        public string RegionName { get; set; }

        public string Description { get; set; }
    }


    public class DepartamentMapData : Departament
    {
        public string MapBounds { get; set; }

        public string MapCenter { get; set; }

        public string MapZoom { get; set; }
    }
}
