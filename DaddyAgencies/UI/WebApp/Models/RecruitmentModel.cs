using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApp.Models
{
    public class RecruitmentModel
    {
        public Guid? Uid { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticUI), ErrorMessageResourceName = nameof(StaticUI.CannotBeEmpty))]
        public string Message { get; set; }

        public List<HttpPostedFileBase> Documents { get; set; }
    }
}