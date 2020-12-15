using System;
using System.Collections.Generic;
using System.Web;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Application.Features;
using MediatR;

namespace DaddyAgencies.Application.Features.Property
{
    public class SaveProperty : SaveRequest, IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string Address { get; set; }

        public decimal SalePrice { get; set; }

        public decimal TotalSquare { get; set; }

        public Guid DepartamentUid { get; set; }

        public Guid PastalCodeUid { get; set; }

        public int? FloorsNumber { get; set; }

        public int PropertyType { get; set; }

        public int? FloorsOutOf { get; set; }

        public int? ParkingType { get; set; }

        public int? ParkingSize { get; set; }

        public int? ParkingCost { get; set; }

        public int? RoomsCount { get; set; }

        public IEnumerable<DucumentBase> Files { get; set; }

        public string Ges { get; set; }
        public string EnergyClass { get; set; }
        public string Status { get; set; }
        public string Parking { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerNote { get; set; }
    }
}
