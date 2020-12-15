using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class InseptionModel
    {
        public string AcceptText { get; set; }

        public bool IsAccepted { get; set; }

        public Guid? PostalCodeUid { get; set; }
        public Guid? PropertyUid { get; set; }
        public Guid RegionUid { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        public string CustomerName { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.InvalidEmail))]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        public int? Time { get; set; }

        public string Description { get; set; }
        public string PropertyLink { get; set; }
        public string PropertyTitle { get; set; }
    }

    public class SaleInseptionModel : InseptionModel
    {
        public List<SelectListItem> PostalCodes { get; set; }
    }
}