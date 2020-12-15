using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminUI.Models
{
    public class UserRoleModel
    {
        public  Guid UserId { get; set; }

        public  Guid RoleId { get; set; }

        public Guid PostalCodeUid { get; set; }

        public Guid[] PostalCodeUids { get; set; }

        public RoleModel Roles { get; set; }
    }

    public class SaleInseptionModel : UserRoleModel
    {
        public List<SelectListItem> PostalCodes { get; set; }
    }
}