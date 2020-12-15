using System;

namespace DaddyAgencies.Application.Models
{
    public class PropertyPreview
    {
        public Guid? Uid { get; set; }

        public string Name { get; set; }
         
        public string Description { get; set; }

        public Guid PostalCodeUid { get; set; }

        public decimal SalePrice { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal TotalSquare { get; set; }

        public string DepartamentName { get; set; }

        public string Address { get; set; }

        public Guid RegionUid { get; set; }

        public Guid DepartamentUid { get; set; }
        public string PostalCode { get; set; }

        public string Ges { get; set; }
        public string EnergyClass { get; set; }
        public string Status { get; set; }
        public string Parking { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerNote { get; set; }
        public int? RoomsCount { get; set; }
        public int? PropertyType { get; set; }
        public int? FloorsNumber { get; set; }
        public int Sortby { get; set; }
    }
}
