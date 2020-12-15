using DaddyAgencies.Common.Util.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminUI.Models
{
    public class DepartamentModel
    {
        [Required(ErrorMessageResourceType = typeof(UI), ErrorMessageResourceName = nameof(UI.CannotBeEmpty))]
        public Guid? RegionModelUid { get; set; }

        public Guid? Uid { get; set; }

        [Required(ErrorMessageResourceType = typeof(UI), ErrorMessageResourceName = nameof(UI.CannotBeEmpty))]
        public string Name { get; set; }

        public string Description { get; set; }

    }

    public class SelectedDeparmentModel : DepartamentModel
    {
        public List<SelectListItem> Regions { get; set; }
    }
}