using DaddyAgencies.Application.Models.Enums;
using System;
using System.Collections.Generic;

namespace DaddyAgencies.Application.Models
{
    public class PropertyFilter
    {
        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 20;
        public int? Sortby { get; set; }
        public string Keyword { get; set; }

        public int PriceMin { get; set; }
        public int PriceMax { get; set; }

        public int SquareMax { get; set; }
        public int SquareMin { get; set; }

        public int RoomsMin { get; set; }
        public int RoomsMax { get; set; }
        public string Parking { get; set; }
        public string Ges { get; set; }
        public string EnergyClass { get; set; }

        public ICollection<Guid> Regions { get; set; }

        public ICollection<Guid> Departaments { get; set; }

        public ICollection<Guid> PostalCodes { get; set; }

        public ICollection<PropertyType> PropertyTypes { get; set; }
    }
}
