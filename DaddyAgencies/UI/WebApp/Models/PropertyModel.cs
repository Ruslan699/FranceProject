using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class PropertyModel
    {
        public Guid? Uid { get; set; }

        public Guid RegionUid { get; set; }

        public string Caption { get; set; }

        public string PostalCode { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public decimal SalePrice { get; set; }

        public int? RoomsCount { get; set; }
        public int? PropertyType { get; set; }
        public string EnergyClass { get; set; }
        public string Ges { get; set; }
        public string Parking { get; set; }

        public decimal TotalSquare { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
        public List<string> PropertyImages { get; set; }
        public List<Guid> PropertyImageIds { get; set; }
    }
}