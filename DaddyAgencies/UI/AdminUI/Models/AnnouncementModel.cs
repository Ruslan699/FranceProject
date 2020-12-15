using DaddyAgencies.Application.Models;
using DaddyAgencies.Common.Util.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminUI.Models
{
    public class AnnouncementModel
    {
        public Guid? Uid { get; set; }

        [Required(ErrorMessageResourceType = typeof(UI), ErrorMessageResourceName = nameof(UI.CannotBeEmpty))]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string Address { get; set; }

        public Guid PostalCode { get; set; }

        public decimal SalePrice { get; set; }

        public decimal TotalSquare { get; set; }
        
        public int PropertyType { get; set; }

        public int? FloorsNumber { get; set; }

        //public int? FloorsOutOf { get; set; }

        //public int? ParkingType { get; set; }

        //public int? ParkingSize { get; set; }

        //public int? ParkingCost { get; set; }

        public int? RoomsCount { get; set; }

        public Guid DepartamentUid { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public string Ges { get; set; }
        public string EnergyClass { get; set; }
        public string Status { get; set; }
        public string Parking { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerNote { get; set; }

        public ICollection<Departament> Departaments { get; set; }
        public ICollection<PostalCode> PostalCodes { get; set; }
        //public List<DucumentBase> Documents { get; set; }
        public List<Guid> ImageIds { get; set; }
    }

    public class SelectedAnnouncement : AnnouncementModel
    {
        public List<SelectListItem> Departments { get; set; }
    }
}