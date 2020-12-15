using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DaddyAgencies.Application.Models;
using DaddyAgencies.Application.Models.Enums;

namespace WebApp.Models
{
    public class PropertyFilter
    {
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


        public ICollection<Guid> SelectedRegions { get; set; }

        public ICollection<Guid> SelectedDepartaments { get; set; }

        public ICollection<Guid> SelectedPostalCodes { get; set; }

        public ICollection<PropertyType> SelectedTypes => GetTypeList();

        #region PropertyType

        public bool IsBuilding { get; set; }
        public bool IsApartment { get; set; }
        public bool IsCottage { get; set; }
        public bool IsArea { get; set; }
        public bool IsParking { get; set; }
        public bool IsOther { get; set; }

        private List<PropertyType> GetTypeList()
        {
            var list = new List<PropertyType>();
            if (IsBuilding)
                list.Add(PropertyType.Building);
            if (IsApartment)
                list.Add(PropertyType.Apartment);
            if (IsCottage)
                list.Add(PropertyType.Cottage);
            if (IsArea)
                list.Add(PropertyType.Area);
            if (IsParking)
                list.Add(PropertyType.Parking);
            if (IsOther)
                list.Add(PropertyType.Other);

            return list;
        }

        #endregion

        public ICollection<SelectListItem> Regions { get; set; }
        public ICollection<SelectListItem> Departaments { get; set; }
        public ICollection<SelectListItem> PostalCodes { get; set; }

        private ICollection<Region> _allRegions;
        public ICollection<Region> AllRegions
        {
            get => _allRegions;
            set
            {
                Regions = new List<SelectListItem>();
                Departaments = new List<SelectListItem>();
                PostalCodes = new List<SelectListItem>();
                _allRegions = value;
                foreach (var region in _allRegions)
                {
                    Regions.Add(new SelectListItem { Value = region.Uid.ToString(), Text = region.Name });
                    foreach (var departament in region.Departaments)
                    {
                        Departaments.Add(new SelectListItem { Value = departament.Uid.ToString(), Text = departament.Name });
                        foreach (var postalCode in departament.PostalCodes)
                        {
                            PostalCodes.Add(new SelectListItem { Value = postalCode.Uid.ToString(), Text = postalCode.Name });
                        }
                    } 
                }

                Regions = Regions.OrderBy(x => x.Text).ToList();
                Departaments = Departaments.OrderBy(x => x.Text).ToList();
                PostalCodes = PostalCodes.OrderBy(x => x.Text).ToList();
            }
        }
    }

    public class PageableFilter : PropertyFilter
    {
        public int Skip { get; set; }

        public int Take { get; set; }
        public int? Sortby { get; set; }
    }
}