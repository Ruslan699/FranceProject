using DaddyAgencies.Common.Util.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminUI.Models
{
    public class RegionModel
    {
        public Guid? Uid { get; set; }

        [Required(ErrorMessageResourceType = typeof(UI), ErrorMessageResourceName = nameof(UI.CannotBeEmpty))]
        public string Name { get; set; }

        public string PathString { get; set; } = "M0;";

        public string Description { get; set; }

        public ICollection<DepartamentModel> Departaments { get; set; }
    }
}