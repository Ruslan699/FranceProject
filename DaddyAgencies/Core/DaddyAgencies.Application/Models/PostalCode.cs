using System;

namespace DaddyAgencies.Application.Models
{
    public class PostalCode
    {
        public Guid? Uid { get; set; }

        public Guid? DepartamentUid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class PostalCodeView : PostalCode
    {
        public string DepartamentName { get; set; }
    }

    public class PostalCodeMapData : PostalCode
    {
        public string PathString { get; set; }

        public string MapBounds { get; set; }

        public string MapCenter { get; set; }

        public string MapZoom { get; set; }
    }
}
